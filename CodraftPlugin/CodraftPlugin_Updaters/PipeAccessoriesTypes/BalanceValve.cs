﻿using Autodesk.Revit.DB;
using CodraftPlugin_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodraftPlugin_Library;
using CodraftPlugin_PipeAccessoriesWPF;
using Newtonsoft.Json.Linq;

namespace CodraftPlugin_Updaters.PipeAccessoriesTypes
{
    public class BalanceValve : BaseAccessory
    {
        public BalanceValve(FamilyInstance accessory, Document doc, string databaseMapPath, JObject file) : base(accessory, doc, databaseMapPath, file)
        {
            this.Query = $"SELECT * " +
                $"FROM {(string)parameterConfiguration["parameters"]["balanceValve"]["property_15"]["database"]} " +
                $"WHERE {(string)parameterConfiguration["parameters"]["balanceValve"]["property_9"]["database"]} = \"{this.Fabrikant}\" " +
                $"AND {(string)parameterConfiguration["parameters"]["balanceValve"]["property_10"]["database"]} = \"{this.Type}\" " +
                $"AND {(string)parameterConfiguration["parameters"]["balanceValve"]["property_16"]["database"]} = {this.Dn}";

            this.QueryCount = $"SELECT COUNT(*) " +
                $"FROM {(string)parameterConfiguration["parameters"]["balanceValve"]["property_15"]["database"]} " +
                $"WHERE {(string)parameterConfiguration["parameters"]["balanceValve"]["property_9"]["database"]} = \"{this.Fabrikant}\" " +
                $"AND {(string)parameterConfiguration["parameters"]["balanceValve"]["property_10"]["database"]} = \"{this.Type}\" " +
                $"AND {(string)parameterConfiguration["parameters"]["balanceValve"]["property_16"]["database"]} = {this.Dn}";
        }

        public override bool? GetParams()
        {
            List<object> parametersList;

            if (FileOperationsPipeAccessories.LookupBalanceValve(Query, QueryCount, ConnectionString, out parametersList))
            {
                if (FileOperations.IsFound(CallingParams, RememberMeFilePath, out List<string> parameters))
                {
                    List<object> correctList = new List<object>();

                    correctList.AddRange(parameters.GetRange(0, 2).Select(x => (object)double.Parse(x)));
                    correctList.AddRange(parameters.GetRange(2, 2).Select(x => (object)int.Parse(x)));
                    correctList.AddRange(parameters.GetRange(4, 4).Select(x => (object)double.Parse(x)));
                    correctList.AddRange(parameters.GetRange(8, 6));

                    this.DatabaseParameters = correctList;
                    return true;
                }

                string typeName = this.ToString();
                string name = typeName.Substring(typeName.LastIndexOf('.') + 1);
                MainWindow accessoryWindow = new MainWindow(PipeAccessory, name, ConnectionString, Query, DatabaseFilePath, CallingParams, parameterConfiguration);
                accessoryWindow.ShowDialog();

                if (accessoryWindow.hasChosenAccessory)
                    return null;

                return false;
            }

            if (!parametersList.Any()) return false;

            this.DatabaseParameters = parametersList;
            return true;
        }

        public override bool ParametersAreTheSame()
        {
            this.RevitParameters.Add(Math.Round(this.PipeAccessory.LookupParameter("Lengte").AsDouble(), 4));
            this.RevitParameters.Add(Math.Round(this.PipeAccessory.LookupParameter("Buitendiameter").AsDouble(), 4));
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("Uiteinde_1_type").AsInteger());
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("Uiteinde_2_type").AsInteger());
            this.RevitParameters.Add(Math.Round(this.PipeAccessory.LookupParameter("Uiteinde_1_maat").AsDouble(), 4));
            this.RevitParameters.Add(Math.Round(this.PipeAccessory.LookupParameter("Uiteinde_2_maat").AsDouble(), 4));
            this.RevitParameters.Add(Math.Round(this.PipeAccessory.LookupParameter("Uiteinde_1_lengte").AsDouble(), 4));
            this.RevitParameters.Add(Math.Round(this.PipeAccessory.LookupParameter("Uiteinde_2_lengte").AsDouble(), 4));
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("COD_Fabrikant").AsString());
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("COD_Type").AsString());
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("COD_Materiaal").AsString());
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("COD_Productcode").AsString());
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("COD_Omschrijving").AsString());
            this.RevitParameters.Add(this.PipeAccessory.LookupParameter("COD_Beschikbaar").AsString());

            return ElementSettings.CompareParameters(this.RevitParameters, this.DatabaseParameters);
        }

        public override void SetWrongValues()
        {
            this.PipeAccessory.LookupParameter("Lengte").Set(10 / feetToMm);
            this.PipeAccessory.LookupParameter("Buitendiameter").Set(10 / feetToMm);
            this.PipeAccessory.LookupParameter("Uiteinde_1_type").Set(0);
            this.PipeAccessory.LookupParameter("Uiteinde_2_type").Set(0);
            this.PipeAccessory.LookupParameter("Uiteinde_1_maat").Set(10 / feetToMm);
            this.PipeAccessory.LookupParameter("Uiteinde_2_maat").Set(10 / feetToMm);
            this.PipeAccessory.LookupParameter("Uiteinde_1_lengte").Set(10 / feetToMm);
            this.PipeAccessory.LookupParameter("Uiteinde_2_lengte").Set(10 / feetToMm);
            this.PipeAccessory.LookupParameter("COD_Fabrikant").Set("BESTAAT NIET!");
            this.PipeAccessory.LookupParameter("COD_Type").Set("BESTAAT NIET!");
            this.PipeAccessory.LookupParameter("COD_Materiaal").Set("BESTAAT NIET!");
            this.PipeAccessory.LookupParameter("COD_Productcode").Set("BESTAAT NIET!");
            this.PipeAccessory.LookupParameter("COD_Omschrijving").Set("BESTAAT NIET!");
            this.PipeAccessory.LookupParameter("COD_Beschikbaar").Set("BESTAAT NIET!");
        }
    }
}
