using UpdateModelLib;

namespace FileUpdateModelLib
{
    public class FileUpdateModel : UpdateModel
    {
        private const string _model = "File";

        public FileUpdateModel()
        {
            base.Model = _model;
        }

        public override event WriteLine UpdateMessage;

        public override void BeforeUpdate()
        {
            UpdateMessage("Before Update");
        }

        public override void Update()
        {
            UpdateMessage(base.Arguments.GetValue<bool>("enable"));
            UpdateMessage("Update");
        }

        public override void AfterUpdate()
        {
            UpdateMessage?.Invoke("After Update");
        }
    }
}
