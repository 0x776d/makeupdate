using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using UpdateModelLib;

namespace FileUpdateModelLib
{
    public class FileUpdateModel : UpdateModel
    {
        private const string _model = "File";

        private bool _isRolledBack;

        private FileUpdateModelConfig _fileUpdateConfig;
        private FileUpdateModelChecker _fileUpdateChecker;
        private FileUpdateModelRestorer _fileUpdateRestorer;
        private FileUpdateModelUpdater _fileUpdateUpdater;

        public FileUpdateModel()
        {
            base.Model = _model;
            _isRolledBack = false;
        }

        public override event WriteLine UpdateMessage;

        public override void LoadArguments()
        {
            _fileUpdateConfig = new FileUpdateModelConfig(base.Arguments);
            _fileUpdateChecker = new FileUpdateModelChecker(_fileUpdateConfig);
            _fileUpdateRestorer = new FileUpdateModelRestorer(_fileUpdateConfig);
            _fileUpdateUpdater = new FileUpdateModelUpdater(_fileUpdateConfig);
        }

        public override void BeforeUpdate()
        {
            UpdateMessage("Before Update");

            if (!_fileUpdateConfig.NoZip)
                _fileUpdateChecker.UnpackSource();

            if (!_fileUpdateConfig.SkipVersionCheck)
                _fileUpdateChecker.CheckVersion();

            if (!_fileUpdateConfig.NoBackup)
                _fileUpdateRestorer.Backup();
        }

        public override void Update()
        {
            UpdateMessage("Update");

            try
            {
                _fileUpdateUpdater.ClearDestination();
                _fileUpdateUpdater.LoadSourceToDestination();
            }
            catch (Exception)
            {
                _isRolledBack = true;
                _fileUpdateRestorer.Rollback();
            }
        }

        public override void AfterUpdate()
        {
            UpdateMessage("After Update");

            _fileUpdateChecker.ClearSource();
            _fileUpdateRestorer.ClearBackup();

            if (!_isRolledBack && _fileUpdateConfig.StartAfterUpdate)
            {
                Process.Start(Path.Combine(_fileUpdateConfig.Destination, _fileUpdateConfig.Program));
                _isRolledBack = false;
            }
        }
    }
}
