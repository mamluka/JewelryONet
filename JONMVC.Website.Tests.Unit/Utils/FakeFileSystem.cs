using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;

namespace JONMVC.Website.Tests.Unit.Utils
{
    class FakeFileSystem
    {
        public static IFileSystem MediaFileSystemForItemNumber(string itemNumber)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")}
                                                        
                                                    });

            return fileSystem;
        }
        public static IFileSystem MediaFileSystemForItemNumber()
        {
            const string itemNumber = "0101-15001";

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>()
                                                    {
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-wg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-wg.flv",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-pic-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-icon-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hand-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-hires-yg.jpg",itemNumber),new MockFileData("")},
                                                        {String.Format(@"C:\Users\maMLUka\Documents\jewelryonnet\internet-sites\jon-images\jewelry\{0}-mov-yg.flv",itemNumber),new MockFileData("")}
                                                        
                                                    });

            return fileSystem;
        }

        public static IFileSystem MediaFileSystemForItemNumber(Dictionary<string, MockFileData> files)
        {
            const string itemNumber = "0101-15001";

            var fileSystem = new MockFileSystem(files);

            return fileSystem;
        }

    }
}
