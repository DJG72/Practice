using System.Collections.Generic;
using MagicFilesLib;
using Moq;
using NUnit.Framework;

namespace DirectoryExplorer.Tests
{
    [TestFixture]
    public class DirectoryExplorerTests
    {
        private readonly string _file1 =
            "file.txt";

        private readonly string _file2 =
            "file2.txt";

        Mock<IDirectoryExplorer> mockDirectoryExplorer;

        [OneTimeSetUp]
        public void Init()
        {
            mockDirectoryExplorer =
                new Mock<IDirectoryExplorer>();
        }

        [TestCase]
        public void GetFiles_ReturnsExpectedFiles()
        {
            List<string> files =
                new List<string>()
                {
                    _file1,
                    _file2
                };

            mockDirectoryExplorer
                .Setup(x =>
                    x.GetFiles(
                        It.IsAny<string>()
                    ))
                .Returns(files);

            ICollection<string> result =
                mockDirectoryExplorer.Object.GetFiles(
                    "D:\\Test"
                );

            Assert.That(
                result,
                Is.Not.Null
            );

            Assert.That(
                result.Count,
                Is.EqualTo(2)
            );

            Assert.That(
                result,
                Does.Contain(_file1)
            );
        }
    }
}