
// JavaScript Document
var Tabs = {
	RegisterFilters: function () {
		$('.sortbar select').bind('change', function() {
			$('form').submit();
		});
	},
	RegisterTabLinks: function () {
		$('#TabNavigation .border div').click(function () {
			window.location.href = $(this).parent().find('span a').attr('href');
		});
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
        
        $('[movie]').click(function() {
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
			    resizable:false
			}
			);

        $('#BestOfferHelp').dialog(
			{
			    autoOpen: false,
			    height: 550,
			    width: 500, 
			    hide: { effect: "fade" },
			    resizable:false
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

        $('a[command="print"]').click(function() {
            window.print();
        });

        $('.emailring').dialog({
                autoOpen: false,
                width:400,
                height:410,
                resizable: false
            });

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
			            $('.emailring').dialog('close');
			            MessageBox.Show('Email sent to friend');
			        }
			    }


			});

        $('a[command="emailring"]').click(function () {
            $('.emailring').dialog('open');
        });


    }

};
MessageBox = {
	Show: function (msg, func) {
	    $.msg({ 
	            bgPath :'/Content/plugins/images/',
	            content: msg,
	            afterBlock: func ,
	            fadeIn:500,
	            fadeOut:200,
	            timeOut: 2000,
	            css:{fontSize:'1.7em'}
	        });
	}
}
var JewelDesign = {
    Locals: {

        DataFromController: null

    },
    FormatSliderPriceRangeInfo: function (id, handleType, value, prefix) {

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
        var toWidth = fromSpan.width() + 6;

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
    FormatSliderCaratRangeInfo: function (from, to) {
        return from + ' Ct. - ' + to + ' Ct.';
    },
    InitDiamondSearch: function () {



        $("#SliderCarat").RmzSlider(
                {
                    minValue: 0.5,
                    maxValue: 3,
                    numHandles: 2,
                    hitMargin: 13,
                    handleWidth: 13,
                    leftOffset: 1
                }
            );

        $("#SliderCarat .handle_num_0").trigger("setvalue", { value: 1 });
        $("#SliderCarat .handle_num_1").trigger("setvalue", { value: 2 });

        JewelDesign.FormatSliderPriceRangeInfo('#SliderCarat', 'to', (2).toFixed(2), '');
        JewelDesign.FormatSliderPriceRangeInfo('#SliderCarat', 'from', (1).toFixed(2), '');

        $("#SliderCarat .handle_num_0").bind("dragging", function (event, data) {

            var from = (data.value).toFixed(2);
            $('#carat1').val(from);

            JewelDesign.FormatSliderPriceRangeInfo('#SliderCarat', 'from', from, '');

        });

        $("#SliderCarat .handle_num_1").bind("dragging", function (event, data) {

            var to = (data.value).toFixed(2);
            $('#carat2').val(to);

            JewelDesign.FormatSliderPriceRangeInfo('#SliderCarat', 'to', to, '');

        });

        $("#SliderCarat").bind("sliderchanged", function (event, data) {

            var from = (data.handles[0].value).toFixed(2);
            var to = (data.handles[1].value).toFixed(2);
            $('#carat2').val(to);
            $('#carat1').val(from);

            JewelDesign.FormatSliderPriceRangeInfo('#SliderCarat', 'to', to, '');
            JewelDesign.FormatSliderPriceRangeInfo('#SliderCarat', 'from', from, '');

            JewelDesign.Search();
        });

        $("#SliderPrice").RmzSlider(
                {
                    minValue: 500,
                    maxValue: 10000,
                    numHandles: 2,
                    hitMargin: 13,
                    handleWidth: 13,
                    leftOffset: 1
                }
            );

        $("#SliderPrice .handle_num_0").trigger("setvalue", { value: 1000 });
        $("#SliderPrice .handle_num_1").trigger("setvalue", { value: 5000 });

        //   $("#SliderPrice").trigger("firechange"); 

        JewelDesign.FormatSliderPriceRangeInfo('#SliderPrice', 'to', (1000).toFixed(0), '$');
        JewelDesign.FormatSliderPriceRangeInfo('#SliderPrice', 'from', (5000).toFixed(0), '$');

        $("#SliderPrice .handle_num_0").bind("dragging", function (event, data) {

            var from = (data.value).toFixed(0);
            $('#Price1').val(from);

            JewelDesign.FormatSliderPriceRangeInfo('#SliderPrice', 'from', from, '$');


        });

        $("#SliderPrice .handle_num_1").bind("dragging", function (event, data) {

            var to = (data.value).toFixed(0);
            $('#Price2').val(to);

            JewelDesign.FormatSliderPriceRangeInfo('#SliderPrice', 'to', to, '$');

        });

        $("#SliderPrice").bind("sliderchanged", function (event, data) {

            var from = (data.handles[0].value).toFixed(0);
            var to = (data.handles[1].value).toFixed(0);
            $('#price2').val(to);
            $('#price1').val(from);

            JewelDesign.FormatSliderPriceRangeInfo('#SliderPrice', 'to', to, '$');
            JewelDesign.FormatSliderPriceRangeInfo('#SliderPrice', 'from', from, '$');

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




        var filters = JewelDesign.ReadSearchFilters();
        jQuery("#DiamondList").jqGrid({




            url: '/JewelDesign/Diamonds',
            datatype: "json",
            postData: filters,
            cache: false,
            loadonce: false,
            //colNames: ['Stone Id', 'Shape', 'Carat', 'Color', 'Clarity', 'Cut', 'Depth', 'Table', 'Pol/Sym', 'Flour', 'Certificate', 'Photo', 'Price', 'Action'],
            colNames: ['Shape', 'Carat', 'Color', 'Clarity', 'Cut', 'Certificate', 'Price', 'Details...'],
            colModel: [
            //{ name: 'stoneid', index: 'id', width: 50 ,resizable:false,sortable:false},
				{name: 'shape', index: 'shape', width: 54, align: 'center', resizable: false, sortable: false },
				{ name: 'weight', index: 'weight', width: 40, align: 'center', resizable: false, sortable: false },
				{ name: 'color', index: 'color', width: 40, align: 'center', resizable: false, sortable: false },
				{ name: 'clarity', index: 'clarity', width: 40, align: 'center', resizable: false, sortable: false },
				{ name: 'cut', index: 'cut', width: 60, align: 'center', resizable: false, sortable: false },
            //{ name: 'depth', index: 'depth', width: 48, align: 'center', resizable: false, sortable: false },
            //{ name: 'table', index: 'table', width: 48, align: 'center', resizable: false, sortable: false },
            //{ name: 'polsym', index: 'polsym', width: 48, resizable: false, sortable: false },
            //{ name: 'flour', index: 'flour', width: 48, resizable: false, sortable: false },
				{name: 'cert', index: 'cert', width: 48, align: 'center', align: 'center', resizable: false, sortable: false },
            //{ name: 'photo', index: 'photo', width: 45, resizable: false, sortable: false },
				{name: 'price', index: 'price', width: 55, align: 'center', resizable: false, sortable: false },
				{ name: 'action', index: 'action', width: 55, align: 'center', resizable: false, sortable: false }
			],
            rowNum: 10,
            rowList: [10, 20, 30],
            //pager: '#pager2',
            sortname: 'id',
            mtype: 'POST',
            viewrecords: true,
            sortorder: "desc",
            loadtext: "Loading diamonds, please wait...",
            caption: "",
            pager: '#DiamondPager',
            height: 220,
            width: 790,
            loadComplete: function (data) {

                $(".jqgrow-notyet").hover(
                    function () {
                        var div = null;
                        if ($('.diamond-info-hover').length > 0) {
                            div = $('.diamond-info-hover');
                        }
                        else {
                            div = $('<div></div>');
                            div.addClass('diamond-info-hover');
                            $('body').append(div);

                            div.hover(
                                function () {
                                    $(this).attr('hover', true);
                                },
                                function () {
                                    $(this).attr('hover', false);
                                    $(this).hide();
                                }

                            );

                            var pos = $('.ui-jqgrid').offset();

                            div.css({
                                left: pos.left - 170 + 'px',
                                top: pos.top + 'px'
                            });
                        }

                        div.show();





                    },
                    function () {
                        
                        var div = null;
                        if ($('.diamond-info-hover').length > 0) {
                            div = $('.diamond-info-hover');
                        }
                        if (div.attr('hover') == 'false') {
                            div.hide();
                        }

                    }
                );

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
                var menu = $(this);
                menu.children(".actions").fadeIn();
            },

            out: function hide() {
                var menu = $(this);
                menu.children(".actions").fadeOut();
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
	EnableSubmitButton: function () {
		$(":submit").removeAttr("disabled");
		$("input[type=image]").removeAttr("disabled");
	},
	DisableSubmitButton: function () {
		$("input[type=image]").attr("disabled", true);
		$(":submit").attr("disabled", true);
	},
	RedirectToHome: function () {
		window.location.href = '/';
	}
};