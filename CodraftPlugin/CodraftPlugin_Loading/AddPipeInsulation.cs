using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;

namespace CodraftPlugin_Loading
{
    [Transaction(TransactionMode.Manual)]
    public class AddPipeInsulation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            DataTable data = null;
            try
            {
                data = Utilities.GetInsulationData(doc);
            }
            catch (IOException ex)
            {
                TaskDialog.Show("Please Close Excel File", "The Excel file is open, Please close it.");
                return Result.Failed;
            }
            catch(Exception ex)
            {
                TaskDialog.Show("Erro", ex.Message);
                return Result.Failed;
            }

            var rows = data.Rows;

            var allPipes = new FilteredElementCollector(doc)
                .OfClass(typeof(Pipe))
                .Cast<Pipe>();

            var insulationTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeInsulations)
                .WhereElementIsElementType()
                .Cast<PipeInsulationType>();

            var allPipeSystems = new FilteredElementCollector(doc)
                .OfClass(typeof(PipingSystemType))
                .WhereElementIsElementType()
                .Cast<PipingSystemType>();

            Transaction t = new Transaction(doc, "Add Insulation To Pipes");
            t.Start();

            var missingPipeSystemTypes = new List<string>();
            var missingInsulationTypes = new List<string>();

            for (int i = 0; i < rows.Count; i++)
            {
                var isInsualtionOrPipeSystemMissing = false;
                var rowData = rows[i].ItemArray;

                string systemType = (string)rowData[0];
                double diameter = UnitUtils.Convert((double)rowData[1], UnitTypeId.Millimeters, UnitTypeId.Feet);
                string insulationType = (string)rowData[2];
                double insulationThickness = UnitUtils.Convert((double)rowData[3], UnitTypeId.Millimeters, UnitTypeId.Feet);

                if (!allPipeSystems.Any(ps => ps.Name == systemType))
                {
                    isInsualtionOrPipeSystemMissing = true;
                    if (!missingPipeSystemTypes.Contains(systemType))
                    {
                        missingPipeSystemTypes.Add(systemType);
                    }
                }

                var pipes = allPipes.Where(p => p.LookupParameter("System Type").AsValueString() == systemType);
                pipes = pipes.Where(p => p.Diameter == diameter);

                var insulation = insulationTypes.FirstOrDefault(x => x.Name == insulationType);
                if (insulation == null)
                {
                    isInsualtionOrPipeSystemMissing = true;
                    if (!missingInsulationTypes.Contains(insulationType))
                    {
                        missingInsulationTypes.Add(insulationType);
                    }
                }

                if (isInsualtionOrPipeSystemMissing)
                {
                    continue;
                }

                foreach (var pipe in pipes)
                {
                    var elemId = pipe.GetDependentElements(new ElementClassFilter(typeof(PipeInsulation))).FirstOrDefault();
                    if (elemId != null)
                    {
                        var insul = doc.GetElement(elemId) as PipeInsulation;
                        insul.Thickness = insulationThickness;
                        var test = insul.GetParameter(ParameterTypeId.ElemTypeParam);
                        insul.GetParameter(ParameterTypeId.ElemTypeParam).Set(insulation.Id);

                        continue;
                    }

                    PipeInsulation.Create(doc, pipe.Id, insulation.Id, insulationThickness);
                }
            }

            t.Commit();

            if (missingPipeSystemTypes.Any() || missingInsulationTypes.Any())
            {
                var dialog = new TaskDialog("Add Pipe Insulation");
                dialog.MainInstruction = "PipeSystemType and/or InsulationType is missing. see below for more details.";
                dialog.ExpandedContent = $"PipeSystemTypes missing: {GetMissingItems(missingPipeSystemTypes)}\nInsulationTypes missing: {GetMissingItems(missingInsulationTypes)}";
                dialog.Show();

                return Result.Failed;
            }

            TaskDialog.Show("Add Pipe Insulation", "Insulation Added Successfully");

            return Result.Succeeded;
        }

        private string GetMissingItems(List<string> items)
        {
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                if (sb.Length != 0)
                {
                    sb.Append(", ");
                }
                sb.Append(item);
            }

            return sb.ToString();
        }
        
    }
}
