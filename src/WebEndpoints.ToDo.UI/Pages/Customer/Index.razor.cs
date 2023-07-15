using ToDo.Domain.CustomerAggregate.Dto;
using Microsoft.AspNetCore.Components;
using WebEndpoints.ToDo.UI.Services;

namespace WebEndpoints.ToDo.UI.Pages.Customer
{
    public partial class Index
    {
        [Inject]
        public CustomerApiClient customerApi { get; set; } = null!;
        private List<CustomerItemDto>? customers;

        protected override async Task OnInitializedAsync()
        {
            customers = await customerApi.GetCustomersAsync();
        }
    }
}
