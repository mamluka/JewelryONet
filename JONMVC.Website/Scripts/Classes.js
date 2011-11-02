
// JavaScript Document
var Tabs = {
    RegisterFilters: function () {
        $('.sortbar select').bind('change', function () {
            $('form').submit();
        });
    },
    RegisterTabLinks: function () {
        $('#TabNavigation .border div').click(function () {
            window.location.href = $(this).parent().find('span a').attr('href');
        });
    },
    RegisterTabsMouseOver: function () {
        $('#TabNavigation .border').hover(
            function() {
                if ($(this).hasClass('active')) return;
                $(this).addClass('mouseover');
            },
            function() {
                if ($(this).hasClass('active')) return;
                $(this).removeClass('mouseover');
            }
         );
    }
};
var Video = {
	OpenMovie: function (moviepath) {

		$('#MoviePlayerDiv').dialog({ width: 670, height: 540, title: '3D Movie', resizable: false });

		$f('MoviePlayer', '/Flash/flowplayer-3.2.7.swf', {

			clip: {
				url: moviepath,
				autoPlay: true,
				loop: true,

				onBeforeFinish: function () { return false; }

			},
			plugins: {
				controls: null
			}


		});



	},
	RegisterHooks: function () {
		$('[movie]').css('cursor', 'pointer');

		$('[movie]').click(function () {
			moviepath = $(this).attr('movie');
			Video.OpenMovie(moviepath);
		});
	}
};
var Item = {
    Locals: { id: 0 },
    RegisterBestOffer: function () {
        $('#BestOfferDialog').dialog(
			{
			    autoOpen: false,
			    height: 180,
			    width: 330,
			    title: 'Place an Offer',
			    resizable: false
			}
			);

        $('#BestOfferHelp').dialog(
            {
                autoOpen: false,
                height: 550,
                width: 500,
                hide: { effect: "fade" },
                resizable: false
            }
			);



        $('.bestoffer img:first').click(function () {
            $('#BestOfferDialog').dialog('open');
        });

        $('.bestoffer a:first').click(function () {
            $('#BestOfferHelp').dialog('open');
        });

        $('#BestOfferForm').ajaxForm(
			{
			    beforeSubmit: function () {
			        return $('#BestOfferForm').validate().showErrors();
			    },
			    dataType: 'json',
			    success: function (data) {
			        if (data.HasError) {
			            alert('There was an error, Please try again later');
			        } else {
			            $('#BestOfferDialog').dialog('close');
			            MessageBox.Show('Offer Recieved');
			        }
			    }


			});

    },
    RegisterPageCommends: function () {
        $('a[command="addtowishlist"]').click(function () {
            $.post(
				'/JewelryItem/AddToWishList',
				{ jewelid: Item.Locals.id },
				function (data) {
				    if (!data.HasError) {
				        MessageBox.Show('Item was added to your wishlist');
				    } else {
				        MessageBox.Show('Error occured, try again later');
				    }
				});
        });

        Item.RegisterAjaxForEmailRing();

        $('a[command="print"]').click(function () {
            window.print();
        });

    },
    LoadRingSizeDropBoxs: function () {
        var Sizes = [
			['4', '1.84', '46.5', 'H ½', '15', '46 ½', '7'],
            ['4.5', '1.89', '47.8', 'I ½', '15 ¼', '47 ¾', '8'],
            ['5', '1.94', '49', 'J ½', '15 ¾', '49', '9'],
            ['5.5', '1.99', '50.2', 'L', '16', '50 ¼', '11'],
            ['6', '2.04', '51.5', 'M', '16 ½', '51 ½', '12'],
            ['6.5', '2.09', '52.8', 'N', '17', '52 ¾', '13'],
            ['7', '2.15', '54', 'N ½', '17 ¼', '54', '14'],
            ['7.5', '2.2', '55.3', 'O ½', '17 ½', '55 ½', '15'],
            ['8', '2.25', '56.6', 'P ½', '18', '56 ¾', '16'],
            ['8.5', '2.3', '57.8', 'Q ½', '18 ½', '58', '17'],
            ['9', '2.35', '59.1', 'R ½', '19', '59 ¼', '18'],
            ['9.5', '2.4', '60.3', 'S ½', '19 ½', '60 ½', '19'],
            ['10', '2.46', '61.6', 'T ½', '20', '61 ¾', '20'],
            ['10.5', '2.5', '62.8', 'U ½', '20 ¼', '62 ¾', '22'],
            ['11', '2.57', '64.1', 'V ½', '20 ¾', '64 ¼', '23'],
            ['11.5', '2.61', '66', 'W ½', '21', '66', '24'],
            ['12', '2.66', '67.2', 'Y', '21 ¾', '67 ¼', '25'],
            ['12.5', '2.69', '68.4', 'Z', '', '', '26'],
            ['13', '2.74', '69.7', 'Z ½', '22', '', '27'],
            ['13.5', '2.8', '71', 'Z +2', '', '', ''],
            ['14', '2.85', '72.5', 'Z +3', '', '', ''],
            ['14.5', '2.91', '74', 'Z +4', '', '', ''],
            ['15', '2.95', '75', 'Z +5', '', '', ''],
		];

        $.each($('.converter select'), function (index, select) {
            $.each(Sizes, function (i, size) {
                $(select).append(new Option(size[index], i, false, false));
            });

            $(select).change(function () {
                var value = $(this).val();
                $('.converter select option').removeAttr('selected');

                $('.converter select option[value=' + value + ']').attr('selected', 'selected');

            });
        });
    },
    RegisterWishList: function () {

        Item.RegisterAjaxForEmailRing();

        $('a[command=wishlist-emailring]').click(function () {
            var id = $(this).attr('id');
            $('#EmailRing input[name=ID]').val(id);
            var boxy = new Boxy('#EmailRing', {
                title: 'Email the ring'
            });
            boxy.show();
        });

        $('a[command=wishlist-print').click(function () {
            window.print();
        });
    },
    RegisterAjaxForEmailRing: function () {

        $('.emailring form').ajaxForm(
			{
			    beforeSubmit: function () {
			        return $('.emailring form').validate().showErrors();
			    },
			    dataType: 'json',
			    success: function (data) {
			        if (data.HasError) {
			            alert('There was an error, Please try again later');
			        } else {

			            Boxy.get($('.emailring')).hide();
			            MessageBox.Show('Email sent to friend');
			            Utils.EnableSubmitButtonInsideAForm('.emailring form');
			        }
			    }


			});
    }


};
MessageBox = {
	Show: function (msg, func) {
		$.msg({
			bgPath: '/Content/plugins/images/',
			content: msg,
			afterBlock: func,
			fadeIn: 500,
			fadeOut: 200,
			timeOut: 2000,
			css: { fontSize: '1.7em' }
		});
	}
}
var JewelDesign = {
    Locals: {

        DataFromController: null

    },
    TranslatePriceSliderValuesToRealPrice: function (val) {

        if (val <= 680) {
            return val * 9.7059 + 400;
        }
        else if (val > 680 && val <= 711) {
            return (val - 680) * 3000 + 7000;
        }
        else if (val > 711) {
            return (val - 711) * 10000 + 100000;
        }

    },
    FormatSliderRangeInfo: function (id, handleType, value, prefix) {

        var fromSpan = null;
        var toSpan = null;
        var range = null;


        if ($(id + ' span.info').length > 0) {
            fromSpan = $(id + ' span.info:eq(0)');
            toSpan = $(id + ' span.info:eq(1)');
            range = $(id + ' div.range');

        }
        else {
            fromSpan = $('<span></span>').addClass('info');
            toSpan = $('<span></span>').addClass('info');
            range = $('<div></div>').addClass('range');

            $(id).append(fromSpan);
            $(id).append(range);
            $(id).append(toSpan);
        }

        if (handleType == 'from') {
            fromSpan.html(prefix + value);
        }
        else if (handleType == 'to') {
            toSpan.html(prefix + value);
        }


        var posFrom = $(id + ' .handle:eq(0)').position();
        var posTo = $(id + ' .handle:eq(1)').position();

        //console.log(posFrom.left);
        // console.log(posTo.left);

        var fromWidth = fromSpan.width() + 6;
        var toWidth = toSpan.width() + 6;

        var totalWidth = $(id).width();




        //TODO make the if's clear coded with functions

        range.css({
            left: posFrom.left + 7 + 'px',
            width: posTo.left - posFrom.left + 'px'

        });



        if (posTo.left - toWidth < posFrom.left + fromWidth && posFrom.left - toWidth > 0 && posTo.left + toWidth < totalWidth) {
            fromSpan.css('left', posFrom.left - fromWidth + 'px');
            toSpan.css('left', posTo.left + 'px');
        }
        else if (posTo.left + toWidth > totalWidth && posTo.left - toWidth < posFrom.left + fromWidth && posTo.left - toWidth + 13 > posFrom.left) {
            fromSpan.css('left', posFrom.left - fromWidth + 'px');
            toSpan.css('left', posTo.left - toWidth + 13 + 'px');
        }
        else if (posTo.left + toWidth > totalWidth && posTo.left - toWidth + 13 <= posFrom.left) {
            fromSpan.css('left', posTo.left - toWidth + 13 - 2 - fromWidth + 'px');
            toSpan.css('left', posTo.left - toWidth + 13 + 'px');
        }
        else if (posFrom.left - fromWidth < 0 && posTo.left - toWidth < posFrom.left + fromWidth && posFrom.left + fromWidth < posTo.left) {
            fromSpan.css('left', posFrom.left + 'px');
            toSpan.css('left', posTo.left + 'px');
        }

        else if (posFrom.left - fromWidth < 0 && posTo.left - toWidth < posFrom.left + fromWidth && posFrom.left + fromWidth >= posTo.left) {
            fromSpan.css('left', posFrom.left + 'px');
            toSpan.css('left', posFrom.left + fromWidth + 2 + 'px');
        }

        else {
            fromSpan.css('left', posFrom.left + 'px');
            toSpan.css('left', posTo.left - toWidth + 13 + 'px');
        }





    },
    InitDiamondSearch: function () {

        $("#SliderCarat").RmzSlider(
				{
				    minValue: 0.25,
				    maxValue: 10,
				    numHandles: 2,
				    hitMargin: 13,
				    handleWidth: 13,
				    leftOffset: 1,
				    step: 0.01
				}
			);

        $("#SliderCarat .handle_num_0").trigger("setvalue", { value: 0.5 });
        $("#SliderCarat .handle_num_1").trigger("setvalue", { value: 2 });


        JewelDesign.FormatSliderRangeInfo('#SliderCarat', 'from', (0.5).toFixed(2), '');
        JewelDesign.FormatSliderRangeInfo('#SliderCarat', 'to', (3).toFixed(2), '');

        $("#SliderCarat .handle_num_0").bind("dragging", function (event, data) {

            var from = (data.value).toFixed(2);
            $('#carat1').val(from);

            JewelDesign.FormatSliderRangeInfo('#SliderCarat', 'from', from, '');

        });

        $("#SliderCarat .handle_num_1").bind("dragging", function (event, data) {

            var to = (data.value).toFixed(2);
            $('#carat2').val(to);

            JewelDesign.FormatSliderRangeInfo('#SliderCarat', 'to', to, '');

        });


        
        
        
        $("#SliderCarat").bind("sliderchanged", function (event, data) {

            var from = (data.handles[0].value).toFixed(2);
            var to = (data.handles[1].value).toFixed(2);
            $('#carat2').val(to);
            $('#carat1').val(from);


            JewelDesign.FormatSliderRangeInfo('#SliderCarat', 'from', from, '');
            JewelDesign.FormatSliderRangeInfo('#SliderCarat', 'to', to, '');

            JewelDesign.Search();
        });

        $("#SliderPrice").RmzSlider(
				{
				    minValue: 0,
				    maxValue: 801,
				    numHandles: 2,
				    hitMargin: 13,
				    handleWidth: 13,
				    leftOffset: 1,
				    step: 1
				}
			);

        $("#SliderPrice .handle_num_0").trigger("setvalue", { value: 0 });
        $("#SliderPrice .handle_num_1").trigger("setvalue", { value: 500 });


        JewelDesign.FormatSliderRangeInfo('#SliderPrice', 'from', (JewelDesign.TranslatePriceSliderValuesToRealPrice(0)).toFixed(0), '$');
        JewelDesign.FormatSliderRangeInfo('#SliderPrice', 'to', (JewelDesign.TranslatePriceSliderValuesToRealPrice(500)).toFixed(0), '$');

        $("#SliderPrice .handle_num_0").bind("dragging", function (event, data) {


            var from = JewelDesign.TranslatePriceSliderValuesToRealPrice(data.value).toFixed(0); //(data.value).toFixed(0);
            $('#Price1').val(from);

            JewelDesign.FormatSliderRangeInfo('#SliderPrice', 'from', from, '$');


        });

        $("#SliderPrice .handle_num_1").bind("dragging", function (event, data) {

            var to = JewelDesign.TranslatePriceSliderValuesToRealPrice(data.value).toFixed(0);
            $('#Price2').val(to);

            JewelDesign.FormatSliderRangeInfo('#SliderPrice', 'to', to, '$');

        });

        $("#SliderPrice").bind("sliderchanged", function (event, data) {

            var from = JewelDesign.TranslatePriceSliderValuesToRealPrice(data.handles[0].value).toFixed(0);
            var to = JewelDesign.TranslatePriceSliderValuesToRealPrice(data.handles[1].value).toFixed(0);
            $('#price2').val(to);
            $('#price1').val(from);

            JewelDesign.FormatSliderRangeInfo('#SliderPrice', 'to', to, '$');
            JewelDesign.FormatSliderRangeInfo('#SliderPrice', 'from', from, '$');

            JewelDesign.Search();
        });


        $('table.bg tr td').click(function () {
            if ($(this).hasClass('on')) {
                $(this).removeClass('on').addClass('off');
            }
            else {
                $(this).removeClass('off').addClass('on');
            }

            JewelDesign.Search();
        });

        $('table.sprite-shape tr td').click(function () {
            if ($(this).hasClass('on')) {
                $(this).removeClass('on').addClass('off');

                var x = 0;
                if ($.browser.msie) {
                    x = parseInt($(this).css('backgroundPositionX').split(" ")[0]);
                } else {
                    x = parseInt($(this).css('backgroundPosition').split(" ")[0]);
                }


                $(this).css('backgroundPosition', x + 'px' + ' 0px');




            }
            else {
                $(this).removeClass('off').addClass('on');
                var x = 0;
                if ($.browser.msie) {
                    x = parseInt($(this).css('backgroundPositionX').split(" ")[0]);
                } else {
                    x = parseInt($(this).css('backgroundPosition').split(" ")[0]);
                }
                $(this).css('backgroundPosition', x + 'px' + ' -25px');
            }

            JewelDesign.Search();
        });

        $.each($('.refine tr td'), function (index, tdelement) {

            if ($(tdelement).attr('key') == JewelDesign.Locals.DataFromController["Shape"]) {
                $(tdelement).trigger('click');
            }

            if ($(tdelement).attr('key') == JewelDesign.Locals.DataFromController["Report"]) {
                $(tdelement).trigger('click');
            }

        });


        $('table tr td').hover(
						function () {
						    $(this).addClass('selected');
						},
						function () {
						    $(this).removeClass('selected');
						}
					 );


        var div = $('.diamond-info-hover');

        div.hover(
						function () {
						    $(this).attr('hover', true);
						},
						function () {
						    $(this).attr('hover', false);
						    $(this).hide();
						}
					);



        div.children('input').click(function () {
            window.location.href = $(this).attr('buttonurl');
        });



        var filters = JewelDesign.ReadSearchFilters();
        jQuery("#DiamondList").jqGrid({
            url: '/JewelDesign/Diamonds',
            datatype: "json",
            postData: filters,
            cache: false,
            loadonce: false,
            colNames: ['Shape', 'Carat', 'Color', 'Clarity', 'Cut', 'Certificate', 'Price', 'Details...'],
            colModel: [
					{ name: 'shape', index: 'shape', width: 54, align: 'center', resizable: false, sortable: false },
					{ name: 'weight', index: 'weight', width: 40, align: 'center', resizable: false, sortable: true },
					{ name: 'color', index: 'color', width: 40, align: 'center', resizable: false, sortable: false },
					{ name: 'clarity', index: 'clarity', width: 40, align: 'center', resizable: false, sortable: false },
					{ name: 'cut', index: 'cut', width: 60, align: 'center', resizable: false, sortable: false },
					{ name: 'cert', index: 'cert', width: 48, align: 'center', align: 'center', resizable: false, sortable: false },
					{ name: 'price', index: 'price', width: 55, align: 'center', resizable: false, sortable: true },
					{ name: 'action', index: 'action', width: 55, align: 'center', resizable: false, sortable: false }
				],
            rowNum: 10,
            rowList: [10, 20, 30],
            sortname: 'id',
            mtype: 'POST',
            viewrecords: true,
            sortorder: "desc",
            loadtext: "Loading diamonds, please wait...",
            caption: "",
            pager: '#DiamondPager',
            height: 220,
            width: 790,
            loadComplete: function () {
                JewelDesign.RegisterDiamondInfo();
            }
        });
        JewelDesign.ReadSearchFilters();


    },
    ReadSearchFilters: function () {

        var obj = {};

        var shapes = $.map($('.refine .on[filter=shape]'), function (n, i) {
            var value = $(n).attr('key');
            obj['Shape[' + i + ']'] = value;
            return value;
        });
        var color = $.map($('.refine .on[filter=color]'), function (n, i) {
            var value = $(n).attr('key');
            obj['Color[' + i + ']'] = value;
            return value;
        });
        var clarity = $.map($('.refine .on[filter=clarity]'), function (n, i) {
            var value = $(n).attr('key');
            obj['Clarity[' + i + ']'] = value;
            return value;
        });
        var cut = $.map($('.refine .on[filter=cut]'), function (n, i) {
            var value = $(n).attr('key');
            obj['Cut[' + i + ']'] = value;
            return value;
        });

        var weightfrom = $('#carat1').val();
        var weightto = $('#carat2').val();
        var pricefrom = $('#price1').val();
        var priceto = $('#price2').val();




        obj['PriceFrom'] = pricefrom;
        obj['PriceTo'] = priceto;
        obj['WeightFrom'] = weightfrom;
        obj['WeightTo'] = weightto;
        obj['page'] = 1;
        obj['settingid'] = JewelDesign.Locals.DataFromController['SettingID'];
        obj['size'] = JewelDesign.Locals.DataFromController['Size'];
        obj['mediatype'] = JewelDesign.Locals.DataFromController['MediaType'];







        return obj;
    },
    Search: function () {
        var filters = JewelDesign.ReadSearchFilters();
        jQuery("#DiamondList").setGridParam({ postData: null }).setGridParam({ postData: filters, 'page': 1 }).trigger("reloadGrid");
    },
    RegisterJewelDesignPagesDropDownMenu: function () {
        $(".buy-menu").hoverIntent({
            sensitivity: 1, // number = sensitivity threshold (must be 1 or higher)
            interval: 50,   // number = milliseconds for onMouseOver polling interval
            timeout: 100,
            over: function show() {
                var submenu = $(this).find(".actions");
                submenu.show();
                submenu.position(
                    {
                        my: 'left top',
                        at: 'left bottom',
                        of: $(this).find('img'),
                        offset: '0 2'
                    }
							);
            },

            out: function hide() {
                var menu = $(this);
                menu.find(".actions").fadeOut();
            }
        });

    },
    RegisterSettingCommandLinks: function () {

        $('a[command=end]').click(function () {
            JewelDesign.SubmitSettingCommand(1);
        });

        $('a[command=choose-diamond]').click(function () {
            JewelDesign.SubmitSettingCommand(2);
        });

        $('a[command=buy-setting]').click(function () {
            JewelDesign.SubmitSettingCommand(3);
        });

    },

    SubmitSettingCommand: function (commandid) {

        $('input[name=Size]').val($('.jewelsize').val());
        $('input[name=CommandID]').val(commandid);

        $('#RedirectSettingForm').submit();


    },
    RegisterDiamondInfo: function () {

        $(".jqgrow").hover(
						function () {
						    var div = null;
						    if ($('.diamond-info-hover').length > 0) {
						        div = $('.diamond-info-hover');
						    }

						    var timerCode = div.attr('timercode');
						    clearTimeout(timerCode);

						    div.show();

						    div.position(
								{
								    my: 'right top',
								    at: 'left top',
								    of: $('.diamond-list-container'),
								    offset: '-11 0'
								}
							);

						    div.attr('hover', 'false');

						    var tip = div.children('.tip');

						    tip.position({
						        my: 'right top',
						        at: 'left top',
						        of: $(this),
						        offset: '-3 3'
						    });

						    var id = $(this).attr('id');
						    var userData = $("#DiamondList").getGridParam('userData');

						    var diamondData = userData[id];

						    $.each(diamondData, function (index, dataItem) {

						        if (dataItem == '') {
						            $('.diamond-info-hover tr[key=' + index.toString().toLowerCase() + ']').hide();
						        } else {
						            $('.diamond-info-hover tr[key=' + index.toString().toLowerCase() + '] td:eq(1)').html(dataItem);
						        }
						    });

						    div.children('input:eq(0)').attr('buttonurl', diamondData['ViewURL']);
						    var settingID = parseInt(JewelDesign.Locals.DataFromController.SettingID);

						    if (settingID > 0) {
						        div.children('input:eq(1)').attr('buttonurl', diamondData['FinishURL']);
						    }
						    else {
						        div.children('input:eq(1)').attr('buttonurl', diamondData['AddURL']);
						    }




						},
						function () {

						    var div = null;
						    if ($('.diamond-info-hover').length > 0) {
						        div = $('.diamond-info-hover');
						    }

						    var timerCode = setTimeout(function () {
						        if (div.attr('hover') == 'false') {
						            div.hide();
						        }
						    }, 500);

						    div.attr('timercode', timerCode);


						}
				);

    }

};
var MetalPriceSelect = {
	RegisterMetalPriceSelectForTabs: function () {
		$('div.metalpricecombo').click(function () {
			//work the ajax magic
			urlstr = '/JewelryItem/MediaSets';
			var itemid = $(this).attr('itemid');
			dataobj = { 'jewelid': itemid };
			$.ajax({
				type: "POST",
				url: urlstr,
				data: dataobj,
				dataType: 'json',
				success: function (data) {

					if (data) {
						var pos = $('div[itemid=' + data.ID + ']').position();
						var box = $('<div></div>').attr({ id: 'MetalPriceComboItems' }).css({ 'left': pos.left, 'top': pos.top + 32 });
						var id = data.ID;

						var price = data.Price;

						var dic = data.MediaSetRouteLinkDictionary;
						$.each(data.MediaSets, function (n, s) {
							var div = $('<div></div>');
							div.hover(
									function () {
										$(this).addClass("hover");
									},
									function () {
										$(this).removeClass("hover");
									}
											);
							div.click($.proxy(function (event) {
								$('div[itemid=' + data.ID + '] .metal').html(this.s.MediaSetFullName);
								$('div[itemid=' + data.ID + ']').parent('.item').find('a img').attr({ 'src': s.IconURLForWebDisplay });
								$('div[itemid=' + data.ID + ']').parent('div.item').children('a').attr({ 'href': dic[s.MediaSetFullName] });
								$(box).remove();
								event.stopPropagation();
							}, { 's': s }));
							div.html('<span class="left metal">' + s.MediaSetFullName + '</span><span class="right price">' + data.Price + '</span><div class="clear" ></div>'); //list.append(div)

							box.append(div);
						});
						$('div[itemid=' + data.ID + ']').append(box);
						$('body').one('click', function () {
							if ($('#MetalPriceComboItems')) {
								$('#MetalPriceComboItems').remove();
							}

						});
					}
				}
			});
		});
	},

	RegisterMetalPriceSelectForJewelryItems: function () {
		$('div.metalpricecombo').click(function () {
			//work the ajax magic
			urlstr = '/JewelryItem/MediaSets';
			var itemid = $(this).attr('itemid');
			dataobj = { 'jewelid': itemid };
			$.ajax({
				type: "POST",
				url: urlstr,
				data: dataobj,
				dataType: 'json',
				success: function (data) {

					if (data) {
						var pos = $('div[itemid=' + data.ID + ']').position();
						var box = $('<div></div>').attr({ id: 'MetalPriceComboItems' }).css({ 'left': pos.left, 'top': pos.top + 32 });
						var id = data.ID;

						var price = data.Price;

						var title = data.Title;



						$.each(data.MediaSets, function (n, s) {
							var div = $('<div></div>');
							div.hover(
									function () {
										$(this).addClass("hover");
									},
									function () {
										$(this).removeClass("hover");
									}
											);
							div.click($.proxy(function (event) {
								//TODO add the way we change the metal after click on the combo

								$('.metal').html(this.s.MediaSetFullName);
								var picPrettyPhotoLink = $('div.media > a ');

								var zoomPrettyPhotoLink = $('div.zoom > a ');

								var zoomImage = zoomPrettyPhotoLink.children('img').attr('src');

								MetalPriceSelect.UpdatePrettyPhotoByLink(picPrettyPhotoLink, s.PictureURLForWebDisplay, s.HiResURLForWebDisplay);
								MetalPriceSelect.UpdatePrettyPhotoByLink(zoomPrettyPhotoLink, zoomImage, s.HiResURLForWebDisplay);

								$('div.thumbs').html(''); //clear the div

								if (s.HiResURLForWebDisplay) {
									MetalPriceSelect.CreateJewelItemPrettyPhotoThumb(s.HiResURLForWebDisplay, title);
								}

								if (s.HandURLForWebDisplay) {
									MetalPriceSelect.CreateJewelItemPrettyPhotoThumb(s.HandURLForWebDisplay, title);
								}

								$("a[rel^='prettyPhoto']").prettyPhoto();

								$(box).remove();
								$('#MediaType').val(s.MediaSet);

								event.stopPropagation();
							}, { 's': s }));
							div.html('<span class="left metal">' + s.MediaSetFullName + '</span><span class="right price">' + data.Price + '</span><div class="clear" ></div>'); //list.append(div)

							box.append(div);
						});
						$('div[itemid=' + data.ID + ']').append(box);
						$('body').one('click', function () {
							if ($('#MetalPriceComboItems')) {
								$('#MetalPriceComboItems').remove();
							}

						});
					}
				}
			});
		});
	},
	CreateJewelItemPrettyPhotoThumb: function (image, alt) {
		var totalThumb = $('<a title="' + alt + '" rel="prettyPhoto[gal]" href="' + image + '"><img src="' + image + '" alt="' + alt + '"></a>');
		$('div.thumbs').append(totalThumb);
	},
	UpdatePrettyPhotoByLink: function (linkobj, thumb, large) {
		linkobj.attr({ href: large });
		linkobj.children('img').attr({ 'src': thumb });
	}

};
var Checkout = {
	RegisterJewelSizeCombo: function () {

		$('.jewelsize').change(function () {
			var size = $(this).val();
			var cartid = $(this).attr('cartid');
			var postdata = { 'cartid': cartid, 'size': size };

			$.ajax({
				type: 'POST',
				url: '/Checkout/JewelSize',
				data: postdata,
				success: function (data) {
					if (data.HasError) {
						alert('Error');
					}
				}
			});
		});



	}
};
Utils = {
    EnableSubmitButtonInsideAForm: function (form) {
        $(form).find(":submit").removeAttr("disabled");
        $(form).find("input[type=image]").removeAttr("disabled");
    },
    DisableSubmitButton: function () {
        $("input[type=image]").attr("disabled", true);
        $(":submit").attr("disabled", true);
    },
    RedirectToHome: function () {
        window.location.href = '/';
    },
    PlaceLoaderNearButton: function (element, location) {

        var loader = $('<img />').attr('src', '/Content/images/skeleton/loading.gif');

        element.parents('div').append(loader);

        loader.position({
            my: 'left center',
            at: location,
            of: element,
            offset:'5 0'
        });


    }



};

$(document).ready(function () {
    $('.find-ring-size').boxy({
        afterShow: function () {
            Item.LoadRingSizeDropBoxs();
        }
    });

    $('.boxy').boxy({
        filter: '#MainMessage'
    });

    $('input[disable-upon-click]').click(function () {

        $(this).removeClass('button').addClass('disabled-button').attr('disabled', 'disabled');

        if ($(this).attr('loader') == 'true') {
            Utils.PlaceLoaderNearButton($(this), $(this).attr('loader-location'));
        }

    });


});

