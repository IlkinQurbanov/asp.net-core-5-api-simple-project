using Linkedin.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Linkedin.Services
{
    public class JsonFileProductService
    {
        //Our constructor
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<Product> GetProducts()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AddRating(string productId, int rating)
{
            var products = GetProducts();
            // LINQ
            var query = products.FirstOrDefault(x => x.Id == productId);

                if (query.Ratings == null)
                {
                    query.Ratings = new int[] { rating };
                }
                else
                {
                    //  var ratingsList = product.Ratings.ToList();
                    //ratingsList.Add(rating);
                    //  product.Ratings = ratingsList.ToArray();
                    var ratings = query.Ratings.ToList();
                    ratings.Add(rating);
                    query.Ratings = ratings.ToArray();

                }
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize(new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }), products);
            }

        }
    }

    }



