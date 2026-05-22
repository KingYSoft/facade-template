# Facade 框架启动模板

这是一个基于 .NET 8、ASP.NET Core 和 ABP 的 Facade 框架启动模板，用于快速创建标准化的后端服务工程。模板内置常用 Web API 基础设施、应用服务分层、领域服务、数据库访问、后台任务、认证、Swagger、日志和多数据库提供程序支持，适合作为业务系统或接口聚合服务的项目脚手架。

## 特性

- 基于 ABP 和 ASP.NET Core 构建
- 支持 JWT 认证
- 支持 SignalR
- 支持 AutoMapper
- 支持 Quartz 定时任务
- 支持 BackgroundJob 后台工作，可按需集成 Hangfire
- 支持 Dapper
- 支持 Oracle、SqlServer、MySql
- 支持 NLog
- 支持 Redis
- 支持 Swagger/OpenAPI，可兼容 Swagger 2.0

## 项目结构

```text
src/
  FacadeCompanyName.FacadeProjectName.Application        应用服务与 DTO 接口
  FacadeCompanyName.FacadeProjectName.DomainService      领域服务、后台任务、设置、菜单与本地化
  FacadeCompanyName.FacadeProjectName.DomainService.Share 共享常量、配置契约与仓储接口
  FacadeCompanyName.FacadeProjectName.Web.Core           Web 基础设施、过滤器、Swagger、认证与控制器基类
  FacadeCompanyName.FacadeProjectName.Web.Host           可运行的 ASP.NET Core Host
  FacadeCompanyName.FacadeProjectName.SqlServer          SqlServer EF Core 上下文、迁移与仓储实现
  FacadeCompanyName.FacadeProjectName.Oracle             Oracle EF Core 上下文、迁移与仓储实现
  FacadeCompanyName.FacadeProjectName.MySql              MySql EF Core 上下文、迁移与仓储实现

test/
  FacadeCompanyName.FacadeProjectName.Tests              自动化测试
```

## 快速开始

1. 安装 Facade 命令行工具：

   ```powershell
   dotnet tool install --global Facade.ToolCLI
   ```

2. 使用模板初始化项目：

   ```powershell
   facade init FacadeDemo
   ```

3. 修改开发环境配置：

   ```text
   src/FacadeCompanyName.FacadeProjectName.Web.Host/appsettings.Development.json
   ```

   根据本地环境配置连接字符串、Redis、认证、日志和数据库提供程序等设置。

4. 还原依赖并运行项目：

   ```powershell
   dotnet restore FacadeCompanyName.FacadeProjectName.sln
   dotnet run --project src/FacadeCompanyName.FacadeProjectName.Web.Host
   ```

## 常用命令

```powershell
# Debug 构建
.\build.ps1

# Release 构建
.\build.ps1 -Configuration Release

# 运行测试
dotnet test FacadeCompanyName.FacadeProjectName.sln
```

## 开发说明

- 公共业务能力优先放在 `Application`、`DomainService` 和 `DomainService.Share` 中。
- Web 相关的通用能力放在 `Web.Core` 中，具体 Host 配置放在 `Web.Host` 中。
- 数据库提供程序相关实现应放在对应的 `SqlServer`、`Oracle` 或 `MySql` 项目中。
- 不要提交生产连接字符串、Token、密钥或机器相关配置。

