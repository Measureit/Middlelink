﻿
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace Middlink.Extensions
{
  public static partial class Extensions
  {
    public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
    {
      var model = new TModel();
      configuration.GetSection(section).Bind(model);
      return model;
    }
  }
}