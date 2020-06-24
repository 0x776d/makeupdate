using ArgumentsLib;
using System;
using System.Collections.Generic;
using UpdateModelLib;

namespace UpdateLib
{
    public class Update : IDisposable
    {
        public event WriteLine UpdateMessage;

        private Arguments _arguments;
        private UpdateConfig _config;
        private UpdateModel _model;
        private Reflector _reflector;

        public Update(UpdateConfig config, IEnumerable<string> args)
        {
            _arguments = new Arguments(config.MarshalerPath, config.Schema, args);
            _reflector = new Reflector(config.ModelPath);

            _config = config;
        }

        public void ExecuteUpdate()
        {
            UpdateMessage("Trying to load update type!");

            LoadUpdateModel();
            ExecuteUpdateModel();
        }

        private void LoadUpdateModel()
        {
            if (!string.IsNullOrWhiteSpace(_config.Model))
                _model = _reflector.GetInstance(_config.Model);
            else
                _model = _reflector.GetInstance(_arguments.GetValue<string>("using"));

            _model.Arguments = _arguments;
            _model.UpdateMessage += UpdateMessage;
        }

        private void ExecuteUpdateModel()
        {
            _model.LoadArguments();

            if (!_config.SkipBeforeUpdate)
                _model.BeforeUpdate();

            if (!_config.SkipUpdate)
                _model.Update();

            if (!_config.SkipAfterUpdate)
                _model.AfterUpdate();
        }

        public void Dispose()
        {
            _model.UpdateMessage -= UpdateMessage;
        }
    }
}
