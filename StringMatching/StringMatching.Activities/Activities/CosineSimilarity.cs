using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StringMatching.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace StringMatching.Activities
{
    /// <summary>
    /// 
    /// </summary>
    [LocalizedDisplayName(nameof(Resources.CosineSimilarity_DisplayName))]
    [LocalizedDescription(nameof(Resources.CosineSimilarity_Description))]
    
    public class CosineSimilarity : ContinuableAsyncCodeActivity
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


        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public CosineSimilarity()
        {
        }

        #endregion


        #region Protected Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (InputString1 == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(InputString1)));
            if (InputString2 == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(InputString2)));

            base.CacheMetadata(metadata);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs

            string val1 = context.GetValue(InputString1);
            string val2 = context.GetValue(InputString2);
            ///////////////////////////
            // Add execution logic HERE
            var result  = new CosineSimilarity().FindCosineSimilarity(val1, val2);

            ///////////////////////////

            // Outputs
            return (ctx) =>
            {
                MacthingIndex.Set(ctx, result);
            };
        }

        /// <summary>
        /// Finds the Cosine similarity value
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        protected string FindCosineSimilarity(string firstString, string secondString)
        {
            //convert string to array
            string[] firstStringArray = firstString.Trim().ToLower().Split(' ').ToArray();
            string[] secondStringArray = secondString.Trim().ToLower().Split(' ').ToArray();

            //Concat both strings
            string[] StringVector = firstStringArray.Concat(secondStringArray).ToArray().Distinct().OrderBy(i => i).ToArray();

            int[] firstVectorVal = new int[StringVector.Length];
            int[] secondVectorVal = new int[StringVector.Length];

            for (int i = 0; i < StringVector.Length; i++)
            {
                //getting matching value from first string 
                var firstStringMatchQuery = from word in firstStringArray
                                            where word.ToLowerInvariant() == StringVector[i].ToLowerInvariant()
                                            select word;


                firstVectorVal[i] = firstStringMatchQuery.Count();

                //getting matching value from second string 
                var secondStringMatchQuery = from word in secondStringArray
                                             where word.ToLowerInvariant() == StringVector[i].ToLowerInvariant()
                                             select word;
                secondVectorVal[i] = secondStringMatchQuery.Count();


            }

            double cosineProduct = GetCosineProduct(firstVectorVal, secondVectorVal);
            double firstVectormagnitude = GetMagnitude(firstVectorVal);
            double SecondVectormagnitude = GetMagnitude(secondVectorVal);

            string result = (cosineProduct / (firstVectormagnitude * SecondVectormagnitude)).ToString("0.###");
            return result;

        }

        /// <summary>
        /// Gets the product of both string
        /// </summary>
        /// <param name="firstVectorVal"></param>
        /// <param name="secondVectorVal"></param>
        /// <returns></returns>
        private static double GetCosineProduct(int[] firstVectorVal, int[] secondVectorVal)
        {
            double cosineProduct = 0;
            for (int i = 0; i < firstVectorVal.Length; i++)
            {
                cosineProduct += (firstVectorVal[i] * secondVectorVal[i]);
            }

            return cosineProduct;
        }

        /// <summary>
        /// Gets the magnitude of each string
        /// </summary>
        /// <param name="currentVectorVal"></param>
        /// <returns></returns>
        private static double GetMagnitude(int[] currentVectorVal)
        {
            return Math.Sqrt(GetCosineProduct(currentVectorVal, currentVectorVal));
        }

        #endregion
    }

    
}

