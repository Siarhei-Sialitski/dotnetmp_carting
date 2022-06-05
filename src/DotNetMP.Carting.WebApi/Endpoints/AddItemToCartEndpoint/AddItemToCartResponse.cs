﻿using DotNetMP.Carting.WebApi.ViewModels;

namespace DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;

public class AddItemToCartResponse
{
    public Guid Id { get; set; }
    public IList<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
}
