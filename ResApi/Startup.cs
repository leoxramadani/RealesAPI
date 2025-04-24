    using System.Text;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using RealesApi.DTA.Intefaces;
    using RealesApi.DTA.Services;
    using RealesApi.DTA.Services.Shared;
    using RealesApi.Extentions;
    using RealesApi.Hubs;
    using RealesApi.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using RealesApi.Helpers.HashService;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using System.IdentityModel.Tokens.Jwt;
    using System;
    using System.Security.Claims;

    namespace RealesApi
    {
        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                    services.AddLogging();

                    services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("realestDb")));

                    services.AddAutoMapper(typeof(AutoMapperProfile));
                    services.AddScoped<IUser, UserService>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<IAuth, AuthService>();
                    services.AddScoped<IProperty, PropertyService>();
                    services.AddScoped<IHashService, HashService>();
                    services.AddScoped<IPropertyType, PropertyTypeService>();
                    services.AddScoped<IPurpose, PurposeService>();
                    services.AddScoped<IConditions, ConditionsServices>();
                    services.AddScoped<IWhatsSpecialPropertyLink, WhatsSpecialPropertyLinkService>();

                    services.AddSignalR();
                    services.AddControllers();
                    services.AddSwaggerGen(options =>
                    {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Res API",
                    });
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id="Bearer",
                                }
                            },
                            new string[]{ }
                        }
                    });
                });

                services.AddCors(options =>
                {
                    options.AddPolicy("AllowKindeOrigins", builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
                });

                services.AddHttpContextAccessor();


                var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
                JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

                ////services.AddAuthentication(options =>
                ////{
                ////    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                ////    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                ////    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                ////})
                ////.AddJwtBearer(options =>
                ////{
                ////    // Critical: Get these values from configuration
                ////    var kindeSettings = Configuration.GetSection("Kinde");
                ////    var domain = kindeSettings["IssuerUrl"] ?? "https://realest.kinde.com";
                ////    var audience = kindeSettings["ClientId"] ?? "8a0a26a46ccb476d8b32b8439a23bb50";

                ////    options.Authority = domain;
                ////    options.RequireHttpsMetadata = true;
                ////    options.SaveToken = true;

                ////    // Use the OIDC discovery document
                ////    options.MetadataAddress = $"{domain}/.well-known/openid-configuration";

                ////    options.TokenValidationParameters = new TokenValidationParameters
                ////    {
                ////        // For debugging only
                ////        ValidateIssuerSigningKey = true,
                ////        ValidateIssuer = true,
                ////        ValidIssuer = domain,
                ////        ValidateAudience = true,
                ////        ValidAudience = audience,
                ////        ValidateLifetime = true,
                ////        // Map standard JWT claims
                ////        NameClaimType = "name",
                ////        RoleClaimType = "role"
                ////    };

                //    options.Events = new JwtBearerEvents
                //    {
                //        OnAuthenticationFailed = context =>
                //        {
                //            Console.WriteLine($"Auth failed: {context.Exception.Message}");
                //            return Task.CompletedTask;
                //        },
                //        OnChallenge = context =>
                //        {
                //            // This will run when authentication fails and a 401 is about to be returned
                //            Console.WriteLine("Challenge issued: Auth required");
                //            return Task.CompletedTask;
                //        },
                //        OnTokenValidated = context =>
                //        {
                //            Console.WriteLine("Token validated");
                //            return Task.CompletedTask;
                //        },
                //        OnMessageReceived = context =>
                //        {
                //            var token = context.Token;
                //            Console.WriteLine($"Auth message received{(token != null ? " with token" : " without token")}");
                //            return Task.CompletedTask;
                //        }
                //    };
                //});


            services.AddAuthorization();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = true;
            }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealesApi v1"));
                }

                app.UseHttpsRedirection();

                app.UseRouting();
                app.UseCors("AllowKindeOrigins");

                app.UseCors("CorsPolicy");

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<OrderHub>("/orderHub");
                });

                //app.Run();
            }
        }
    }
