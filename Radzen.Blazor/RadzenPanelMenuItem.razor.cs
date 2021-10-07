﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Radzen.Blazor
{
    public partial class RadzenPanelMenuItem : RadzenComponent
    {
        protected override string GetComponentCssClass()
        {
            return "rz-navigation-item";
        }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public EventCallback<bool> ExpandedChanged { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public object Value { get; set; }

        [Parameter]
        public string Path { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string Image { get; set; }

        [Parameter]
        public RenderFragment Template { get; set; }

        [Parameter]
        public bool Expanded { get; set; } = false;

        [Parameter]
        public bool Selected { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        async System.Threading.Tasks.Task Toggle()
        {
            expanded = !expanded;
            await ExpandedChanged.InvokeAsync(Expanded);
            StateHasChanged();
        }

        string getStyle()
        {
            string deg = expanded ? "180" : "0";
            return $@"transform: rotate({deg}deg);";
        }

        string getItemStyle()
        {
            return expanded ? "" : "display:none";
        }

        void Expand()
        {
            expanded = true;
        }

        RadzenPanelMenu _parent;

        [CascadingParameter]
        public RadzenPanelMenu Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (_parent != value)
                {
                    _parent = value;
                    _parent.AddItem(this);
                }
            }
        }

        RadzenPanelMenuItem _parentItem;

        [CascadingParameter]
        public RadzenPanelMenuItem ParentItem
        {
            get
            {
                return _parentItem;
            }
            set
            {
                if (_parentItem != value)
                {
                    _parentItem = value;
                    _parentItem.AddItem(this);

                    EnsureVisible();
                }
            }
        }

        List<RadzenPanelMenuItem> items = new List<RadzenPanelMenuItem>();

        public void AddItem(RadzenPanelMenuItem item)
        {
            if (items.IndexOf(item) == -1)
            {
                items.Add(item);
                StateHasChanged();
            }
        }

        public void Select(bool value)
        {
            Selected = value;

            StateHasChanged();
        }

        void EnsureVisible()
        {
            if (Selected)
            {
                var parent = ParentItem;

                while (parent != null)
                {
                    parent.Expand();
                    parent = parent.ParentItem;
                }
            }
        }

        private bool expanded = false;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.DidParameterChange(nameof(Expanded), Expanded))
            {
                expanded = parameters.GetValueOrDefault<bool>(nameof(Expanded));
            }

            await base.SetParametersAsync(parameters);
        }
        public async System.Threading.Tasks.Task OnClick(MouseEventArgs args)
        {
            if (Parent != null)
            {
                await Parent.Click.InvokeAsync(new MenuItemEventArgs() { Text = this.Text, Path = this.Path, Value = this.Value });
            }
        }
    }
}