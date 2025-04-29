using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CodraftPlugin_Loading
{
    public class TagUpdater : IUpdater
    {
        private Guid _guid = new Guid("4BE213A2-B5B6-43E3-A6E9-62821B4F5427");
        private UpdaterId _updaterId;
        public TagUpdater(AddInId addinId)
        {
            this._updaterId = new UpdaterId(addinId, _guid);
        }
        public void Execute(UpdaterData data)
        {
            if(data.GetAddedElementIds().Count > 1)
            {
                return;
            }

            Document doc = data.GetDocument();
            TaskDialog.Show("test", "hahah");
        }

        public string GetAdditionalInformation()
        {
            return "TagUpdater";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.Annotations;
        }

        public UpdaterId GetUpdaterId()
        {
            return _updaterId;
        }

        public string GetUpdaterName()
        {
            return "TagUpdater";
        }
    }
}
