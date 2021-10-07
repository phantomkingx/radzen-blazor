using Microsoft.AspNetCore.Components;
using System;

namespace Radzen.Blazor
{
    public class RadzenTreeLevel : ComponentBase
    {
        [Parameter]
        public string TextProperty { get; set; }

        [Parameter]
        public string ChildrenProperty { get; set; }

        [Parameter]
        public Func<object, bool> HasChildren { get; set; } = value => true;

        [Parameter]
        public Func<object, bool> Expanded { get; set; } = value => false;

        [Parameter]
        public Func<object, bool> Selected { get; set; } = value => false;
    
        [Parameter]
        public Func<object, string> Text { get; set; }

        [Parameter]
        public RenderFragment<RadzenTreeItem> Template { get; set; }

        [CascadingParameter]
        public RadzenTree Tree 
        { 
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                value.AddLevel(this);
            }
        }
    }
}