﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InfoSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Models;

public class Profile
{
    public Guid Id { get; set; }
}