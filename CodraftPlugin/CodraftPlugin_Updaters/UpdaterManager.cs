using Autodesk.Revit.DB;
using CodraftPlugin_Loading;

namespace CodraftPlugin_Updaters
{
    public class UpdaterManager
    {
        /// <summary>
        /// Registers the pipeupdater with all of its triggers.
        /// </summary>
        /// <param name="addinId"></param>
        public static void RegisterPipeUpdater(AddInId addinId)
        {
            Pipes pipeUpdater = new Pipes(addinId);
            UpdaterId id = pipeUpdater.GetUpdaterId();

            UpdaterRegistry.RegisterUpdater(pipeUpdater);

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeElementAddition());

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_START_OFFSET_PARAM)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_END_OFFSET_PARAM)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.ELEM_TYPE_PARAM)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM)));

            UpdaterRegistry.DisableUpdater(id);
        }

        /// <summary>
        /// Unregister the pipeupdater
        /// </summary>
        /// <param name="addinId"></param>
        public static void UnregisterPipeUpdater(AddInId addinId)
        {
            Pipes pipeUpdater = new Pipes(addinId);
            UpdaterId id = pipeUpdater.GetUpdaterId();

            UpdaterRegistry.RemoveAllTriggers(id);
            UpdaterRegistry.UnregisterUpdater(id);
        }

        /// <summary>
        /// Registers the fitting updater with all of its triggers
        /// </summary>
        /// <param name="addinId"></param>
        public static void RegisterFittingUpdater(AddInId addinId)
        {
            Fittings fittingUpdater = new Fittings(addinId);
            UpdaterId id = fittingUpdater.GetUpdaterId();

            UpdaterRegistry.RegisterUpdater(fittingUpdater);

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting),
                Element.GetChangeTypeElementAddition());

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting),
                Element.GetChangeTypeAny());

            UpdaterRegistry.DisableUpdater(id);
        }

        /// <summary>
        /// Unregisters the fitting updater
        /// </summary>
        /// <param name="addinId"></param>
        public static void UnregisterFittingUpdater(AddInId addinId)
        {
            Fittings fittingUpdater = new Fittings(addinId);
            UpdaterId id = fittingUpdater.GetUpdaterId();

            UpdaterRegistry.RemoveAllTriggers(id);
            UpdaterRegistry.UnregisterUpdater(id);
        }

        /// <summary>
        /// Registers the insulation updater with all of its triggers
        /// </summary>
        /// <param name="addinId"></param>
        public static void RegisterInsulationUpdater(AddInId addinId)
        {
            Insulation insulationUpdater = new Insulation(addinId);
            UpdaterId id = insulationUpdater.GetUpdaterId();

            UpdaterRegistry.RegisterUpdater(insulationUpdater);

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeElementAddition());

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeInsulations),
                Element.GetChangeTypeElementAddition());

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_PIPE_OUTER_DIAMETER)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.ELEM_TYPE_PARAM)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_PIPING_SYSTEM_TYPE_PARAM)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_REFERENCE_INSULATION_THICKNESS)));

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves),
                Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.RBS_REFERENCE_INSULATION_TYPE)));

            UpdaterRegistry.DisableUpdater(id);
        }

        /// <summary>
        /// Unregisters the insulation updater
        /// </summary>
        /// <param name="addinId"></param>
        public static void UnregisterInsulationUpdater(AddInId addinId)
        {
            Insulation insulationUpdater = new Insulation(addinId);
            UpdaterId id = insulationUpdater.GetUpdaterId();

            UpdaterRegistry.RemoveAllTriggers(id);
            UpdaterRegistry.UnregisterUpdater(id);
        }

        /// <summary>
        /// Registers the pipeAccessory updater with all of its triggers
        /// </summary>
        /// <param name="addinId"></param>
        public static void RegisterPipeAccessoryUpdater(AddInId addinId)
        {
            PipeAccessories pipeAccessoriesUpdater = new PipeAccessories(addinId);
            UpdaterId id = pipeAccessoriesUpdater.GetUpdaterId();

            UpdaterRegistry.RegisterUpdater(pipeAccessoriesUpdater);

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeAccessory),
                Element.GetChangeTypeElementAddition());

            UpdaterRegistry.AddTrigger(id,
                new ElementCategoryFilter(BuiltInCategory.OST_PipeAccessory),
                Element.GetChangeTypeAny());

            UpdaterRegistry.DisableUpdater(id);
        }

        /// <summary>
        /// Unregisters the pipeAccessory updater
        /// </summary>
        /// <param name="addinId"></param>
        public static void UnregisterPipeAccessoryUpdater(AddInId addinId)
        {
            PipeAccessories pipeAccessoriesUpdater = new PipeAccessories(addinId);
            UpdaterId id = pipeAccessoriesUpdater.GetUpdaterId();

            UpdaterRegistry.RemoveAllTriggers(id);
            UpdaterRegistry.UnregisterUpdater(id);
        }

        public static void RegisterTagUpdater(AddInId addinId)
        {
            TagUpdater tagUpdater = new TagUpdater(addinId);
            UpdaterId id = tagUpdater.GetUpdaterId();

            UpdaterRegistry.RegisterUpdater(tagUpdater);

            ///TODO add triggers for all tags
            UpdaterRegistry.AddTrigger(id, new ElementCategoryFilter(BuiltInCategory.OST_WallTags),
                Element.GetChangeTypeElementAddition());

            UpdaterRegistry.DisableUpdater(id);
        }

        public static void UnregisterTagUpdater(AddInId addinId)
        {
            TagUpdater tagUpdater = new TagUpdater(addinId);
            UpdaterId tagId = tagUpdater.GetUpdaterId();

            UpdaterRegistry.RemoveAllTriggers(tagId);
            UpdaterRegistry.UnregisterUpdater(tagId);
        }

        /// <summary>
        /// Set the order of execution of the updaters.
        /// </summary>
        /// <param name="first">first to execute</param>
        /// <param name="second">second to execute</param>
        public static void SetExecutionOrder(AddInId addinId)
        {
            UpdaterRegistry.SetExecutionOrder(new Pipes(addinId).Id, new Fittings(addinId).Id);
        }
    }
}
