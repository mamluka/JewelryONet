
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
	FormatSliderPriceRangeInfo: function (from, to) {
		return '$' + from + ' - ' + '$' + to;
	},
	FormatSliderCaratRangeInfo: function (from, to) {
		return from + ' Ct. - ' + to + ' Ct.';
	},
	InitDiamondSearch: function () {
		$("#SliderCarat").slider({
			range: true,
			min: 10,
			max: 350,
			values: [50, 150],
			change: function (event, ui) {

				var from = $(this).slider('values', 0);
				var to = $(this).slider('values', 1);
				$('#carat1').val(from / 100);
				$('#carat2').val(to / 100);

				//   $('#SliderCarat .ui-slider-range-info').html(JewelDesign.FormatSliderPriceRangeInfo(from,to))

				JewelDesign.Search();
			},
			create: function (event, ui) {
				var from = $(this).slider('values', 0) / 100;
				var to = $(this).slider('values', 1) / 100;
				var div = $('<div></div>').addClass('ui-slider-range-info').html(JewelDesign.FormatSliderCaratRangeInfo(from, to));
				$('#SliderCarat .ui-slider-range').append(div);
			},
			slide: function (event, ui) {
				var from = $(this).slider('values', 0) / 100;
				var to = $(this).slider('values', 1) / 100;
				$('#SliderCarat .ui-slider-range-info').html(JewelDesign.FormatSliderCaratRangeInfo(from, to));
			}
		});

		$("#SliderPrice").slider({
			range: true,
			min: 300,
			max: 10000,
			values: [2000, 5000],
			change: function (event, ui) {

				var from = $(this).slider('values', 0);
				var to = $(this).slider('values', 1);

				$('#price1').val(from);
				$('#price2').val(to);

				//  $('#SliderPrice .ui-slider-range-info').html(JewelDesign.FormatSliderPriceRangeInfo(from,to))

				JewelDesign.Search();
			},
			create: function (event, ui) {
				var from = $(this).slider('values', 0);
				var to = $(this).slider('values', 1);

				var div = $('<div></div>').addClass('ui-slider-range-info').html(JewelDesign.FormatSliderPriceRangeInfo(from, to));


				$('#SliderPrice .ui-slider-range').append(div);
			},
			slide: function (event, ui) {
				var from = $(this).slider('values', 0);
				var to = $(this).slider('values', 1);

				$('#SliderPrice .ui-slider-range-info').html(JewelDesign.FormatSliderPriceRangeInfo(from, to));
			}
		});



		$('#SliderPrice a:nth-child(3)').addClass('ui-slider-handle-right');
		$('#SliderCarat a:nth-child(3)').addClass('ui-slider-handle-right');

		$('table.bg tr td').click(function () {
			if ($(this).hasClass('on')) {
				$(this).removeClass('on').addClass('off');
			}
			else {
				$(this).removeClass('off').addClass('on');
			}

			JewelDesign.Search();
		});
		$('table.bg-34 tr td').click(function () {
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
					x= parseInt($(this).css('backgroundPosition').split(" ")[0]);
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
				$(this).css('backgroundPosition', x + 'px' + ' -34px');
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
			colNames: [ 'Shape', 'Carat', 'Color', 'Clarity', 'Cut',  'Certificate', 'Price', 'Details...'],
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
			width: 710


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
		var report = $.map($('.refine .on[filter=report]'), function (n, i) {
			var value = $(n).attr('key');
			obj['Report[' + i + ']'] = value;
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

		$('a[command=end]').click(function() {
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