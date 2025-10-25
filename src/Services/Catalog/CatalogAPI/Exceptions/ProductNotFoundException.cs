// namespace CatalogAPI.Exceptions;

// public class ProductNotFoundException : Exception
// {
//     public ProductNotFoundException() : base("Product not found"){}
// }

﻿using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {
    }
}