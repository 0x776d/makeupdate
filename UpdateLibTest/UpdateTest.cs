using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.Text;
using UpdateLib;
using UpdateModelLib;
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
    }
}
