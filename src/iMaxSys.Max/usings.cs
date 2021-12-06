//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: usings.cs
//摘要: 全局using
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

global using System;
global using System.IO;
global using System.Net;
global using System.Data;
global using System.Data.Common;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.Unicode;
global using System.Text.Encodings;
global using System.Text.Encodings.Web;
global using System.Text.RegularExpressions;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Reflection;
global using System.Collections;
global using System.Collections.Generic;
global using System.Security.Cryptography;
global using System.Net.Mime;
global using System.Net.Http.Headers;
global using System.Drawing;
global using System.Drawing.Drawing2D;
global using System.Drawing.Imaging;
global using System.ComponentModel;
global using System.Collections.Concurrent;
global using System.Runtime.Loader;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.Mvc.ApplicationModels;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyModel;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Query;
global using Microsoft.EntityFrameworkCore.Query.Internal;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.ValueGeneration;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using AutoMapper;
global using StackExchange.Redis;

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Collection;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Max.Json.NamingPolicy;