// RmzSlider v1.1 / horizontal slider for jQuery
//----------------------------------------------
//
// In case you want to do prototypish inheritance, feel free to rip the code out
// and modify it to work like MySlider.prototype = new RmzSlider... or smg. similar.
//
// This is a simple lightweight slider written by Szabolcs Kurdi
// and is released under the MIT license.
// (szabolcs.kurdi AT gmail.com || www.rosamez.com) 
//
// NOTES ON DOM ELEMENTS
//=======================
// the innerHTML of the target will look like this:
// <div class='button button_left'></div>
// <div class='button button_left'></div>
// <div class='handle handle_num_%N'></div>
//
// the buttons are optional (addButtons = true); position for the container
// is changed to relative, inner items are absolute positioned for this parent.
// Everything else shall be styled with css, the classnames are hardcoded.
// The active handle (the one being dragged) gets the class "active".
//
//
// NOTES ON COMMUNICATION / INTERFACES:
//======================================
// The communication with the component is done via custom events
//
// you can LISTEN for events on
//
// 1. the container itself: sliderchanged
//    returns {handles: [array of objects with value and percent]}
//    EXAMPLE:
//    $("#myslider").bind("sliderchanged", 
//      function(event, data){console.log("slider changed: ", data);});
//
// 2. on the handle dom elements: dragstart, dragging, changed
//    these return an object: {value: Number, percent: Number}
//    percents are always >= 0 <= 1
//    EXAMPLE:
//    $("#myslider .handle_num_0").bind("dragstart", 
//      function(event, data){console.log("dragstart: ", data);});
//
//
// you can TRIGGER certain events on
//
// 1. the container: setparams
//    you can modify the P (params) object and refresh the handles,
//    for further information see the onContSetParams function
//    EXAMPLE:
//    $("#myslider").trigger("setparams",
//      {step: 50, refresh: true, maxValue: 400});
//
// 2. on the handle dom elements: setvalue
//    for further information see the setValue function
//    EXAMPLE:
//    $("#myslider .handle_num_2").trigger("setvalue",
//      {value: 380, animate: true});
//
// 3. on the container one may force a changed trigger: firechange
//
// HISTORY
//=========
//
// 1.1: containerLeft set to dynamic, clientX to pageX fix (window resizes)
// 1.0: initial release
//

(function ($) {
    $.fn.RmzSlider = function (params) {

        var RmzSliderTemplate = function (target, params) {

            var P = {
                //offsetleft of the container; -1 for autodetect,
                //though if the container is initially hidden, it will not work
                //like most autodetection routines bellow
                containerLeft: -1, //DEPRECATED, 

                //center of the handle; the mouse snaps to this coordinate, may be -1
                handleCenter: -1,

                //left offset; if you want to have a button on the left/right
                //then the size of that
                leftOffset: 0,

                //add left and right button containers (with leftOffsetSize)
                addButtons: false,

                //width of the zone in which the handle can be dragged, -1 autodetect
                //this is the effective inner area (container with - buttons' size)
                //NOTE: if autodetection fails, use hardcoded values pls
                width: -1,

                //min and max values
                minValue: 0,
                maxValue: 100,

                //width of the handle, this should be set, though autodetection may work
                handleWidth: -1,

                //the hit (bumper) margin of a handle; -1 will disable collision detection
                //use a low value if you want the sliders to overlap slightly
                hitMargin: -1,

                //the delta value for the +- buttons (on the left/right);
                //this is a standard value, not a percentage
                step: 10,

                //either a number (one slider only) or preferably an array;
                //empty array may be used, hadles are distributed automatically
                initialValues: [],

                //number of handles
                numHandles: 1,

                //snap to edge distance in pixels; this is useful for large
                //ranges and small sliders where the to pixel conversion
                //looses values/ranges.
                snapDistance: 3
            };
            var that = this;

            this.init = function () {
                var i, leftv;

                //preset values, merge params, create internals
                $.extend(true, P, params);
                that._calculateParams();

                //$.extend(true, P, params);
                that.values = new Array(P.numHandles);
                that._handlePositions = new Array(P.numHandles);
                that._lastValue = 0;
                that._lastTouched = 0;

                //add handles
                target.css({ position: "relative" });
                for (i = 0; i < P.numHandles; i++) {
                    target.append("<div class='handle handle_num_" + i + "'></div>");
                }

                if (P.handleWidth == -1) {
                    P.handleWidth = target.find(".handle").width();
                    that._calculateParams(true);
                }

                //add buttons
                if (P.addButtons) {
                    target.append("<div class='button button_left'></div>" +
						"<div class='button button_right'></div>");
                    target.find(".button").css({ position: "absolute", width: P.leftOffset, left: 0 });
                    target.find(".button_right").css({ left: target.width() - P.leftOffset });
                }

                //add observers, events to handles
                target.find(".handle").each(function (m) {
                    $(this).css({ position: "absolute" });
                    $(this).bind("mousedown", m, that.onMouseDown);
                    $(this).bind("setvalue", m, that.setValue);

                    if ((typeof (P.initialValues) == "number") && (P.numHandles == 1)) {
                        leftv = P.initialValues;
                    } else {
                        leftv = P.initialValues[m] ? P.initialValues[m] :
							((0 + 1 / (P.numHandles - 1) * m) * (P.maxValue - P.minValue) + P.minValue);
                    }
                    $(this).trigger("setvalue", { value: leftv, noFire: true });
                });

                //container events
                //target.click(that.onContClick);
                target.bind("setparams", params, that.onContSetParams);
                target.bind("firechange", params, that.onContFireChange);
            };


            // EVENTS
            //================================

            //on container click:
            //see if the click was inside or on the buttons,
            //fake buttons (bubble up) may change values
            this.onContClick = function (event) {
                if ($(event.target).hasClass("handle")) return;
                var cleft = that._containerLeft() + P.leftOffset,
					cright = cleft + P.width - P.handleCenter,
					clickx = event.pageX,
					pos = event.pageX - that._containerLeft() - P.handleCenter,
					neg = (clickx < cleft) ? -1 : 1;

                if ((clickx >= cleft) && (clickx <= cright)) {
                    if (pos <= P.leftOffset) pos = P.leftOffset;
                    if (pos >= P.width) pos = P.width;
                    target.find(".handle_num_" + that._lastTouched)
						.trigger("setvalue", { value: that._leftToValue(pos, event.data).value, animate: 1 });
                } else if (P.leftOffset && P.addButtons) {//fake buttons
                    target.find(".handle_num_" + that._lastTouched)
						.trigger("setvalue", { value: neg * P.step, relative: 1, animate: 1 });
                }
            };

            // (RE)SET PARAMS
            // 
            // called by the setparams event, besides the usual params
            // (P object) an extra property needs to be added
            // for refreshing: refresh.
            // 
            // Refresh when ranges, sizes, locations are modified;
            // refresh will NOT recalculate autodetect values (-1).
            //
            // example for external parameter modification:
            // $("#newslider").trigger("setparams", {step: 50, maxValue: 400, refresh: true});
            this.onContSetParams = function (event, params) {
                P = $.extend(true, P, params);
                var saved = that.values.slice();
                if (params.refresh) {
                    (target).find(".handle").each(function (m) {
                        $(this).trigger("setvalue", { percent: saved[m].percent, noFire: true });
                    });
                }
            };

            //fire a change
            this.onContFireChange = function (event, params) {
                target.trigger("sliderchanged", { handles: that.values });
            };

            //document binds, store last touched, fire dragstart
            this.onMouseDown = function (event) {
                that.dragging = true;
                $(event.target).addClass("active");
                $(document).bind("mousemove.sliderdrag", event.data, that.onMouseMove);
                $(document).bind("mouseup.sliderdrag", event.data, that.onMouseUp);
                that._lastTouched = event.data;
                that._lastValue = that.values[event.data].value;
                $(event.target).trigger("dragstart", that.values[event.data]);
                return false;
            };

            //do constraint checks, move there, fire dragging
            this.onMouseMove = function (event) {
                if (!that.dragging) return false;

                var pos = event.pageX - that._containerLeft() - P.handleCenter;


                //if ((pos <= P.leftOffset) || (pos >= P.width + P.leftOffset)) return false;
                if ((P.hitMargin > -1) && (pos != that._getHits(pos, event.data))) return false;
                pos = that._minMaxLocks(pos);

                var posForVal = pos + Math.pow(-1, event.data) * P.handleCenter;

                that.values[event.data] = that._leftToValue(posForVal, event.data);

                that._handlePositions[event.data] = pos;
                target.find(".handle_num_" + event.data).css({ left: pos });
                target.find(".handle_num_" + event.data).trigger("dragging", that.values[event.data]);
                return false;
            };

            //drag finished, unbind functions
            this.onMouseUp = function (event) {
                var realEl = target.find(".handle_num_" + event.data);
                realEl.removeClass("active");
                $(document).unbind(".sliderdrag");
                realEl.trigger("dragfinished", that.values[event.data]);
                if (that._lastValue != that.values[event.data].value) {
                    realEl.trigger("changed", that.values[event.data]);
                    target.trigger("sliderchanged", { handles: that.values });
                }
            };

            // SETVALUE
            // 
            // setvalue may be used both internally and externally;
            // for external piping raise a custom event on a slider.
            // Valid object properties: value, percent, animate, relative, noFire.
            // 
            // for example:
            // $("#newslider .handle_num_2").trigger("setvalue", {value: 380, animate: true});
            this.setValue = function (event, data) {
                var percent = (typeof (data.percent) != "undefined") ? true : false,
					val = (typeof (data) == "number") ? data : (data.value || data.percent),
					range = P.maxValue - P.minValue, i, pos;

                if (!val) val = 0;
                if (data.relative) {
                    val = that.values[that._lastTouched].value + val;
                }

                //if percent, convert to real value
                if (percent) {
                    if (val > 1) val = val / 100;
                    val = that._constraints(val, 0, 1);
                    val = P.minValue + (P.maxValue - P.minValue) * val;
                }

                //finnally, we have the desired value, now we need the pixel pos
                val = that._constraints(val);


                if (!range) range = 1;
                pos = P.leftOffset + (val - P.minValue) / range * (P.width - P.handleWidth - P.handleCenter);

                //can a handle hit another one?
                if ((P.hitMargin > -1) && (!data.noFire)) {
                    pos = that._getHits(pos, event.data);
                }
                pos = that._minMaxLocks(Math.round(pos));

                //store the final values
                that.values[event.data] = that._leftToValue(pos, event.data);
                that._handlePositions[event.data] = pos;
                //finally move there and fire events
                if (data.animate) {
                    target.dequeue();
                    $(event.target).animate({ left: pos });
                } else {
                    $(event.target).css({ left: pos });
                }
                if (!data.noFire) {
                    $(event.target).trigger("changed", that.values[event.data]);
                    target.trigger("sliderchanged", { handles: that.values });
                }
            };


            // PUBLIC GETTERS / SETTERS
            // (not used with jquery plugin)
            //================================

            //returns an array of objects (with value, percent)
            this.getValues = function () {
                return that.values;
            };

            //simply returns the value of the first handle
            this.getValue = function () {
                return that.values[0].value;
            };


            // INTERNAL HELPERS
            //================================

            //do range constraints for a given number
            this._constraints = function (num, min, max) {
                if (arguments.length == 1) {
                    min = P.minValue;
                    max = P.maxValue;
                }
                if (num < min) return min;
                else if (num > max) return max;
                else return num;
            };

            //convert left px value to real values
            this._leftToValue = function (left, handleNumber) {
                var w = P.width - 2 * P.handleWidth - P.handleCenter,
					p = left - P.leftOffset - P.handleCenter,
					percent, value;

                if (!p) percent = 0;
                else if (p == w) percent = 1;
                else percent = p / w;

               

                value = P.minValue + (P.maxValue - P.minValue) * percent;
                return { value: value, percent: percent };
            };

            //detects collision among the handles;
            //returns the new collision-safe left coordinate for handle
            this._getHits = function (pos, edatNum) {
                var 
					oldpos = that._handlePositions[edatNum],
					allpos = [], allposUniq = [], temp = -1, hitpos, bumpers = [];

                allpos = that._handlePositions.slice(); //all positions
                allpos.push(P.leftOffset - P.hitMargin); //leftmost edge
                allpos.push(P.width + P.leftOffset); //rightmost edge
                allpos = allpos.sort(function (a, b) { return a - b }); //sort inc
                for (i = 0; i < allpos.length; i++) {//make unique
                    if ((temp != allpos[i]) && (typeof (allpos[i]) == "number")) allposUniq.push(allpos[i]);
                    temp = allpos[i];
                }
                allpos = allposUniq;
                hitpos = $.inArray(oldpos, allpos); //get place in array
                bumpers.push(hitpos > 0 ? allpos[hitpos - 1] : allpos[0]); //pervious neighbour (or very first)
                bumpers.push(allpos[hitpos + 1] ? allpos[hitpos + 1] : allpos[allpos.length - 1]); //next neighbour (or very last)
                return that._constraints(pos, bumpers[0] + P.hitMargin, bumpers[1] - P.hitMargin);
                /*
                //this algorithm while simpler, will check the original neighbours,
                //which is not very good if you change collision detection on the fly.
                //I leave it here though.
                var 
                toNum = function(s,fall){return s ? (s.replace(/px/,""))*1 : fall},
                bump = [
                toNum(target.find(".handle_num_"+(edatNum-1)).css("left"), P.leftOffset - P.hitMargin) + P.hitMargin,
                toNum(target.find(".handle_num_"+(edatNum+1)).css("left"), P.width + P.leftOffset) - P.hitMargin
                ];
                return that._constraints(pos, bump[0], bump[1]);
                */
            };

            this._minMaxLocks = function (pos) {
                if (pos < P.leftOffset + P.snapDistance) pos = P.leftOffset;
                if (pos > P.leftOffset + P.width - P.snapDistance - P.handleWidth - P.handleCenter)
                    pos = P.width + P.leftOffset - P.handleWidth - P.handleCenter;
                return pos;
            };

            this._calculateParams = function (re) {
                if ((P.handleWidth > -1) && (P.handleCenter == -1)) P.handleCenter = Math.round(P.handleWidth / 2);
                if (that._containerLeft() == -1) that._containerLeft() = target.offset().left;
                if ((P.width == -1) || (re)) P.width = target.width() - P.leftOffset * 2;

                P.width += P.handleCenter;

            };

            //PATCH: always get the container width
            this._containerLeft = function () {
                return target.offset().left;
            };


            // FINAL BOOTSTRAP
            //================================

            this.init();
        };

        return this.each(function () {
            new RmzSliderTemplate($(this), params);
        });
    };

})(jQuery);           //end of cl