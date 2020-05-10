using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
//using StringMatching.Activities.Design.Designers;
using StringMatching.Activities.Design.Properties;

namespace StringMatching.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            //builder.AddCustomAttributes(typeof(CosineSimilarity), categoryAttribute);
            //builder.AddCustomAttributes(typeof(CosineSimilarity), new DesignerAttribute(typeof(Designers.CosineSimilarity)));
            //builder.AddCustomAttributes(typeof(CosineSimilarity), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
