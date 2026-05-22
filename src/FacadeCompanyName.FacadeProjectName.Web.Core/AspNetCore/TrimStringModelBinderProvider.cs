using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.AspNetCore
{
    public class TrimStringModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(string))
            {
                return new TrimStringModelBinder();
            }

            return null;
        }
    }
}