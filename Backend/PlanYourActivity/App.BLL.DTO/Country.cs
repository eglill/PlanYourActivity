﻿using Base.Domain;

namespace App.BLL.DTO;

public class Country : DomainEntityId
{
    public string Name { get; set; } = default!;
}
