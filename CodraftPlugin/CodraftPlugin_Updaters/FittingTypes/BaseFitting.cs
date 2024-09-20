using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Newtonsoft.Json.Linq;

namespace CodraftPlugin_Updaters.FittingTypes
{
    public class BaseFitting
    {
        public string ConnectionString { get; private set; }
        public string TextFilesMapPath { get; set; }
        public string DatabaseFilePath { get; set; }
        public string RememberMeFile { get; set; }
        public string DatabaseFile { get; set; }
        public FamilyInstance Fi { get; set; }
        public Document Doc { get; set; }
        public ElementId Id { get; set; }
        public string SystemType { get; set; }
        public JObject parametersConfiguration { get; set; }

        public BaseFitting(FamilyInstance fitting, Document doc, string databaseMapPath, string textFilesMapPath, JObject file)
        {
            this.Fi = fitting;
            this.Doc = doc;
            this.Id = fitting.Id;
            this.SystemType = fitting.get_Parameter(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM).AsValueString();
            this.SystemType = GetDatabaseName();

            this.TextFilesMapPath = textFilesMapPath;
            this.RememberMeFile = textFilesMapPath + "RememberMe_New.txt";
            this.DatabaseFile = this.SystemType + ".accdb";
            this.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseMapPath}{this.DatabaseFile}";
            this.DatabaseFilePath = databaseMapPath + this.DatabaseFile;
            this.parametersConfiguration = file;
        }

        private string GetDatabaseName()
        {
            ConnectorSet connecters = Fi.MEPModel.ConnectorManager.Connectors;

            foreach (Connector connector in connecters)
            {
                foreach (Connector con in connector.AllRefs)
                {
                    if (con.Owner is Pipe pipe)
                    {
                        return pipe.Name.Substring(0, pipe.Name.IndexOf('%'));
                    }
                }
            }

            return string.Empty;
        }
    }
}
