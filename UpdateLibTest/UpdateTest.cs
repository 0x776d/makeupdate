using ArgumentsLib;
using System;
using System.Collections.Generic;
using UpdateLib;
using Xunit;

namespace UpdateLibTest
{
    public class UpdateTest
    {
        UpdateConfig _config;
        IEnumerable<string> _args;

        public UpdateTest()
        {
            _config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = @".\Model",
                Model = "File",
                Schema = "enable,text*"
            };

            _args = new string[] { "-enable", "-text", "Das ist ein Test" };
        }

        [Fact]
        public void UpdateCreateReference_PassingTest()
        {
            Update update = new Update(_config, _args);

            Assert.NotNull(update);
        }

        [Fact]
        public void UpdateCreateReferenceWithNullUpdateConfig_FailingTest()
        {
            Update update;
            Assert.Throws<NullReferenceException>(() => update = new Update(null, _args));
        }

        [Fact]
        public void UpdateCreateReferenceWithNullArgs_FailingTest()
        {
            Update update;
            Assert.Throws<ArgumentNullException>(() => update = new Update(_config, null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateCreateReferenceWithEmptyMarshalerPath_FailingTest(string marshalerPath)
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = marshalerPath,
                ModelPath = @".\Model",
                Model = "File",
                Schema = "enable,text*"
            };

            Update update;
            Assert.Throws<LibraryArgumentException>(() => update = new Update(config, _args));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateCreateReferenceWithEmptyModelPath_FailingTest(string modelPath)
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = modelPath,
                Model = "File",
                Schema = "enable,text*"
            };

            Update update;
            Assert.Throws<LibraryException>(() => update = new Update(config, _args));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateCreateReferenceWithEmptyModel_PassingTest(string model)
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = @".\Model",
                Model = model,
                Schema = "enable,text*"
            };

            Update update = new Update(config, _args);
            Assert.NotNull(update);
        }

        [Fact]
        public void UpdateCreateReferenceWithEmptySchema_FailingTest()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = @".\Model",
                Model = "File",
                Schema = ""
            };

            Update update;
            Assert.Throws<LibraryArgumentException>(() => update = new Update(config, _args));
        }

        [Fact]
        public void UpdateCreateReferenceWithNullSchema_FailingTest()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = @".\Model",
                Model = "File",
                Schema = null
            };

            Update update;
            Assert.Throws<NullReferenceException>(() => update = new Update(config, _args));
        }
    }
}
