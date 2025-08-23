var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CorpResource_API>("corpresource-api");

builder.AddProject<Projects.CorpResource>("corpresource");

builder.AddProject<Projects.CorpResource_Workers>("corpresource-workers");

builder.Build().Run();
