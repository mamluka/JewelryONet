using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JONMVC.Website.Extensions
{
    public static class prettyPhoto
    {
        public static IHtmlString prettyPhotoSingleImage(this HtmlHelper html, string thumb, string large, string alt)
        {
            var abuilder = new TagBuilder("a");
            abuilder.Attributes.Add(new KeyValuePair<string, string>("href",large));
            abuilder.Attributes.Add(new KeyValuePair<string, string>("rel", "prettyPhoto"));
            abuilder.Attributes.Add(new KeyValuePair<string, string>("title",alt));
            

            var imgbuilder = new TagBuilder("img");

            imgbuilder.Attributes.Add(new KeyValuePair<string, string>("src",thumb));
            imgbuilder.Attributes.Add(new KeyValuePair<string, string>("alt",alt));

            abuilder.InnerHtml = imgbuilder.ToString(TagRenderMode.SelfClosing);
            
            return MvcHtmlString.Create(abuilder.ToString(TagRenderMode.Normal)) ;


        }
        public static IHtmlString prettyPhotoSingleImage(this HtmlHelper html, prettyPhotoMedia media)
        {
            var abuilder = new TagBuilder("a");
            abuilder.Attributes.Add(new KeyValuePair<string, string>("href", media.LargePhoto));
            abuilder.Attributes.Add(new KeyValuePair<string, string>("rel", "prettyPhoto"));
            abuilder.Attributes.Add(new KeyValuePair<string, string>("title", media.Alt));


            var imgbuilder = new TagBuilder("img");

            imgbuilder.Attributes.Add(new KeyValuePair<string, string>("src", media.Thumb));
            imgbuilder.Attributes.Add(new KeyValuePair<string, string>("alt", media.Alt));

            abuilder.InnerHtml = imgbuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(abuilder.ToString(TagRenderMode.Normal));


        }

        public static IHtmlString prettyPhotoPictureGallery(this HtmlHelper html, prettyPhotoMedia media,string galleryPrefix)
        {
            var abuilder = new TagBuilder("a");
            abuilder.Attributes.Add(new KeyValuePair<string, string>("href", media.LargePhoto));

            var rel = String.Format("prettyPhoto[{0}]", galleryPrefix);

            abuilder.Attributes.Add(new KeyValuePair<string, string>("rel", rel));
            abuilder.Attributes.Add(new KeyValuePair<string, string>("title", media.Alt));


            var imgbuilder = new TagBuilder("img");

            imgbuilder.Attributes.Add(new KeyValuePair<string, string>("src", media.Thumb));
            imgbuilder.Attributes.Add(new KeyValuePair<string, string>("alt", media.Alt));

            abuilder.InnerHtml = imgbuilder.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(abuilder.ToString(TagRenderMode.Normal));


        }  
    }

    public class prettyPhotoMediaFactory
    {
        public prettyPhotoMedia SinglePhoto(string thumb, string large, string alt)
        {
            if (String.IsNullOrWhiteSpace(thumb) )
            {
                throw new  ArgumentException("Thump path was empty or null");
            }
            if (String.IsNullOrWhiteSpace(large))
            {
                throw new  ArgumentException("large Image path was empty or null");
            }
            return new prettyPhotoMedia(thumb,large,alt);
        }

        public prettyPhotoMedia SinglePhotoUseLargeForBoth(string large, string alt)
        {
            return new prettyPhotoMedia(large, large, alt);
        }
    }

    public class prettyPhotoMedia
    {
        public prettyPhotoMedia(string thumb, string large, string alt)
        {
            Thumb = thumb;
            LargePhoto = large;
            Alt = alt;
        }

        public string Thumb { get; private set; }
        public string LargePhoto { get; private set; }
        public string Alt { get; private set; }

        public override bool Equals(object obj)
        {
            var other = (prettyPhotoMedia) obj;

            if (other.Alt == Alt && other.LargePhoto == LargePhoto && other.Thumb == Thumb)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Thumb + LargePhoto).GetHashCode();
        }
    }
}