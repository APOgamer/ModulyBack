﻿namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class RemoveStockResource
{
    public Guid UserId { get; set; }
    public Guid BeingId { get; set; }
    public int Quantity { get; set; }
}