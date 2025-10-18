var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Supabase API Client Setup
builder.Services.AddHttpClient("Supabase", client =>
{
    client.BaseAddress = new Uri("https://aqggznupasapaglgfagi.supabase.co/rest/v1/");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("apikey", "sb_publishable_DZeMaqXgLnG6iJUL2LzuSA_i75ywx46");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer sb_publishable_DZeMaqXgLnG6iJUL2LzuSA_i75ywx46");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthorization();
app.MapRazorPages();
app.Run();
