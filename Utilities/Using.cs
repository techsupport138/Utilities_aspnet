﻿global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Utilities_aspnet.Utilities.Entities;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Utilities_aspnet.Utilities.Dtos;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using Utilities_aspnet.IdTitle;
global using Utilities_aspnet.Product;
global using Utilities_aspnet.Content;
global using Utilities_aspnet.User.Dtos;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Hosting;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;
global using StackExchange.Redis;
global using Utilities_aspnet.Utilities.Data;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using Microsoft.AspNetCore.Http.Features;
global using Utilities_aspnet.Form;
global using Microsoft.OpenApi.Models;
global using System.Text.RegularExpressions;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Utilities_aspnet.Utilities;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Utilities_aspnet.Report;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Hosting;
global using RestSharp;
global using Utilities_aspnet.Vote.Entities;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Swashbuckle.AspNetCore.SwaggerUI;
global using System.Security.Principal;
global using Utilities_aspnet.FollowBookmark;
global using Microsoft.AspNetCore.Authorization;
global using Utilities_aspnet.Notification;