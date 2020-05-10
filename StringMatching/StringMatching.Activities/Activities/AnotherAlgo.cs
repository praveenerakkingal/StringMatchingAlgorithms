using StringMatching.Activities.Properties;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace StringMatching.Activities.Activities
{
    class AnotherAlgo : ContinuableAsyncCodeActivity
    {

        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedCategory(nameof(Resources.Input_Category))]
        [LocalizedDisplayName(nameof(Resources.InputString1_DisplayName))]
        [LocalizedDescription(nameof(Resources.InputString1_Description))]
        public InArgument<string> InputString1 { get; set; }


        [LocalizedCategory(nameof(Resources.Input_Category))]
        [LocalizedDisplayName(nameof(Resources.InputString2_DisplayName))]
        [LocalizedDescription(nameof(Resources.InputString2_Description))]
        public InArgument<string> InputString2 { get; set; }

        [LocalizedCategory(nameof(Resources.Output_Category))]
        [LocalizedDisplayName(nameof(Resources.MacthingIndex_DisplayName))]
        [LocalizedDescription(nameof(Resources.MacthingIndex_Description))]
        public OutArgument<string> MacthingIndex { get; set; }
        #endregion

        public override InArgument<bool> ContinueOnError { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



    }
}
