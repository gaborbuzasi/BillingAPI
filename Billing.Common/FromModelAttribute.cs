using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Billing.Common
{
    public sealed class FromModelAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource { get; } = BindingSource.ModelBinding;
    }
}
