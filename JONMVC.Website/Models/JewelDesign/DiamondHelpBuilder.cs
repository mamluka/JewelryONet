using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JONMVC.Website.Models.Diamonds;
using JONMVC.Website.Models.Utils;
using JONMVC.Website.ViewModels.Views;

namespace JONMVC.Website.Models.JewelDesign
{
    public class DiamondHelpBuilder
    {
        private readonly XDocument source;

        public DiamondHelpBuilder(IXmlSourceFactory xmlSourceFactory)
        {
            source = xmlSourceFactory.DiamondHelpSource();
        }

        public Dictionary<string, DiamondHelpViewModel> Build(Diamond diamond)
        {
            var dic = new Dictionary<string, DiamondHelpViewModel>();

            dic["cut"] = CreateModelFor("cut", diamond.Cut);
            dic["color"] = CreateModelFor("color", diamond.Color);
            dic["clarity"] = CreateModelFor("clarity", diamond.Clarity);

            return dic;
        }

        public DiamondHelpViewModel CreateModelFor(string key,string currentValue)
        {
            var helppage = source.Root.Elements().Where(x => x.Attribute("key").Value == key).SingleOrDefault();
            if (helppage == null)
            {
                throw new Exception("When asked to load the diamond help for:" + key + " the help key was not found");
            }
            var viewModel = new DiamondHelpViewModel();
            
            viewModel.Title = helppage.Attribute("title").Value;
            var bodyTextElement =
                helppage.Elements("helppart").Where(x => x.Attribute("value").Value == currentValue).SingleOrDefault();
            if (bodyTextElement != null)
            {
                viewModel.BodyText = bodyTextElement.Value;
            }
            else
            {
                viewModel.BodyText = "N/A";
            }
            
            viewModel.CurrentValueOfHelp = currentValue;
            viewModel.HelpValues = helppage.Elements("helppart").Select(x => x.Attribute("value").Value).ToList();

            return viewModel;

        }
            
    }
}