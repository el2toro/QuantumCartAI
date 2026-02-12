using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class ProductModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a5a9e340-9c8b-43c6-ac90-04162e2a8109"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f2592271-553c-48b2-a8b9-38c751d3567e"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("0a1819eb-0f56-417b-aecb-08c038047248") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("7c8006ab-4dbe-40b2-8876-08fa113cf986"), new Guid("0c02c7af-5bcf-4166-8c9a-974e4e5222ec") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), new Guid("15077ff2-a784-4746-9ba4-5f26cb28bf11") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("21853cd8-c563-4266-a0a3-80576ccbd164") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("2d787418-ff8f-4d6d-b29d-3d2b7c062ecf") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), new Guid("378690c5-295d-4955-8860-abbed9c2c9ab") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("def16ffb-17bb-45b6-a192-e10143a6eaa4"), new Guid("4d03f9af-1097-414a-b704-213a95e36b72") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), new Guid("540a8135-cbd8-4bb3-858d-a3347cc9d16d") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("84d834cd-d647-4768-9350-94327748afcd"), new Guid("7293599c-6113-4303-b1c9-01c81e762166") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("796073c9-87e2-41b6-b742-f58c4ce8311f") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("7c7a8e8e-6926-422a-9e8c-232932e4d47d") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("debcc794-3754-4e80-a031-5e771f2dfc27"), new Guid("936f8bec-3b59-4b87-a315-a5914c3c9af7") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("9fe80821-3cf3-48f4-b674-1a8079beea59") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), new Guid("a2b9d992-e3e4-4e9b-8f2a-1ceff976d5ab") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), new Guid("a9278579-5a6e-4b5d-9693-78eca5817d05") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), new Guid("b31becfe-cd73-42dd-98be-171c20f672a7") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("d65d91ae-99e4-48c4-a77a-cd495cb9761f") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("84d834cd-d647-4768-9350-94327748afcd"), new Guid("dcb09d99-4e56-4bfe-bbb9-34fa8f67c56c") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("e66d1bec-0a22-47a5-8b3e-ce7eadc94863") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("f53ec6d4-0927-4b42-8799-b7824549c53d") });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("40d4a750-812b-4364-9027-676bea89216d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7c8006ab-4dbe-40b2-8876-08fa113cf986"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("84d834cd-d647-4768-9350-94327748afcd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("debcc794-3754-4e80-a031-5e771f2dfc27"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("def16ffb-17bb-45b6-a192-e10143a6eaa4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0a1819eb-0f56-417b-aecb-08c038047248"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0c02c7af-5bcf-4166-8c9a-974e4e5222ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("15077ff2-a784-4746-9ba4-5f26cb28bf11"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("21853cd8-c563-4266-a0a3-80576ccbd164"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2d787418-ff8f-4d6d-b29d-3d2b7c062ecf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("378690c5-295d-4955-8860-abbed9c2c9ab"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4d03f9af-1097-414a-b704-213a95e36b72"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("540a8135-cbd8-4bb3-858d-a3347cc9d16d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7293599c-6113-4303-b1c9-01c81e762166"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("796073c9-87e2-41b6-b742-f58c4ce8311f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7c7a8e8e-6926-422a-9e8c-232932e4d47d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("936f8bec-3b59-4b87-a315-a5914c3c9af7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9fe80821-3cf3-48f4-b674-1a8079beea59"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a2b9d992-e3e4-4e9b-8f2a-1ceff976d5ab"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a9278579-5a6e-4b5d-9693-78eca5817d05"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b31becfe-cd73-42dd-98be-171c20f672a7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d65d91ae-99e4-48c4-a77a-cd495cb9761f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dcb09d99-4e56-4bfe-bbb9-34fa8f67c56c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e66d1bec-0a22-47a5-8b3e-ce7eadc94863"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f53ec6d4-0927-4b42-8799-b7824549c53d"));

            migrationBuilder.RenameColumn(
                name: "ImageFile",
                table: "Products",
                newName: "Image");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), "Apparel for women", "Women's Clothing" },
                    { new Guid("66e3349d-f52a-47d0-a4f3-def5cbc871d6"), "Comfortable home clothing", "Loungewear" },
                    { new Guid("754a24e0-2591-4910-a181-ce1af6716e77"), "Underwear, pajamas, nightwear", "Underwear & Sleepwear" },
                    { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), "Belts, hats, scarves, bags", "Accessories" },
                    { new Guid("a3f85702-9ad8-4d83-bdc0-8a4355fa3548"), "Jackets, coats", "Outerwear" },
                    { new Guid("a7b37a81-c4c7-416e-abc5-4539b9e3d0fb"), "Shoes, boots, sneakers", "Footwear" },
                    { new Guid("cc17f01d-4acd-4b2b-8800-3ff8a9a912ce"), "Suits, dresses, gowns", "Formal Wear" },
                    { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), "Apparel for men", "Men's Clothing" },
                    { new Guid("f4252c2b-a491-4104-aa76-1412b193811f"), "Activewear, gym clothes", "Sportswear" },
                    { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), "Apparel for children", "Kids' Clothing" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Image", "IsAvailable", "Name", "Price", "Quantity", "Rating", "Sku", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("14836d91-3a5b-443a-8c4c-c69ead6bd09e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Denim, slim fit, dark wash", "mens-slim-jeans.jpg", true, "Men's Slim Jeans", 49.99m, 0, 5, "MEN-JEANS-02", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("16f91ae1-ad43-4741-bf1a-e75d57418dbd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft cotton, colorful design", "kids-hoodie.jpg", true, "Kids' Hoodie", 29.99m, 0, 4, "KID-HOODIE-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("226d0753-f1f2-45d1-9da7-2dedfa68c097"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft cotton top and pants", "loungewear-set.jpg", true, "Loungewear Set", 59.99m, 0, 4, "LOUNGE-SET-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("290148e2-985e-4fe1-b59e-9d2e77401cdd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breathable material, adjustable", "sports-cap.jpg", true, "Sports Cap", 14.99m, 0, 4, "ACC-CAP-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("30940b9a-d383-457c-b14c-0d8823cedd36"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight, floral print", "womens-summer-dress.jpg", true, "Women's Summer Dress", 39.99m, 0, 5, "WOM-DRESS-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("46892596-7481-4727-9741-4ea753f02c80"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tailored fit, premium fabric", "mens-suit.jpg", true, "Men's Suit", 249.99m, 0, 5, "FORM-SUIT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("53e2055b-3c93-40d9-b1aa-b78105ad9652"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warm and stylish", "mens-hoodie.jpg", true, "Men's Hoodie", 59.99m, 0, 4, "MEN-HOODIE-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("55b3439e-7afc-427e-9d7e-b470063d4c04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual and sporty", "womens-sneakers.jpg", true, "Sneakers for Women", 79.99m, 0, 4, "WOM-SNEAK-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("586cea93-e806-4660-89da-2ddc892fd208"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof and insulated", "kids-winter-boots.jpg", true, "Kids' Winter Boots", 59.99m, 0, 5, "KID-BOOT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("59d041a9-50f4-47d6-b2c4-081f9cf73336"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "100% cotton, comfortable fit", "mens-classic-tshirt.jpg", true, "Men's Classic T-Shirt", 25.99m, 0, 4, "MEN-TSHIRT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("692f2039-e00e-4657-94a3-d3db49d93015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight with cushioned sole", "mens-running-shoes.jpg", true, "Men's Running Shoes", 89.99m, 0, 5, "MEN-RUN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7325fc9c-c587-4367-941f-18a12580e3bf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft and cozy sleepwear", "womens-pajama-set.jpg", true, "Women's Pajama Set", 42.99m, 0, 5, "UNDER-WOM-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("77229aaf-e0b6-40c5-9f3a-166553ca4fdf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elegant floor-length gown", "womens-evening-gown.jpg", true, "Women's Evening Gown", 199.99m, 0, 5, "FORM-GOWN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("81868517-1a49-4f41-a296-0f26936b8f7e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight and durable", "kids-sneakers.jpg", true, "Kids' Sneakers", 34.99m, 0, 5, "KID-SNEAK-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9861557a-7d5b-480b-b0b9-e077c066857b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silk blend, long sleeves", "womens-blouse.jpg", true, "Women's Blouse", 35.50m, 0, 4, "WOM-BLOUSE-02", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9bf7190d-22c6-4752-a93e-fc6e5bcf183a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof and insulated", "winter-jacket.jpg", true, "Winter Jacket", 129.99m, 0, 5, "OUT-JACKET-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b166874f-6799-475a-b322-cd12f226d547"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable nightwear", "mens-pajama-set.jpg", true, "Men's Pajama Set", 39.99m, 0, 4, "UNDER-MEN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c6f3ab4f-de29-40d7-833f-fbf48e68de09"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stretchable and comfortable", "womens-yoga-pants.jpg", true, "Women's Yoga Pants", 45.00m, 0, 4, "WOM-YOGA-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cf6ad59b-f991-4404-a3a3-6fe765b5f570"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Durable and spacious", "backpack.jpg", true, "Backpack", 49.99m, 0, 4, "ACC-BAG-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f533a0d0-eaeb-48ca-bc30-e348066dc4c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Genuine leather, classic buckle", "leather-belt.jpg", true, "Leather Belt", 19.99m, 0, 4, "ACC-BELT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("14836d91-3a5b-443a-8c4c-c69ead6bd09e") },
                    { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), new Guid("16f91ae1-ad43-4741-bf1a-e75d57418dbd") },
                    { new Guid("66e3349d-f52a-47d0-a4f3-def5cbc871d6"), new Guid("226d0753-f1f2-45d1-9da7-2dedfa68c097") },
                    { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), new Guid("290148e2-985e-4fe1-b59e-9d2e77401cdd") },
                    { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("30940b9a-d383-457c-b14c-0d8823cedd36") },
                    { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("46892596-7481-4727-9741-4ea753f02c80") },
                    { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("53e2055b-3c93-40d9-b1aa-b78105ad9652") },
                    { new Guid("a7b37a81-c4c7-416e-abc5-4539b9e3d0fb"), new Guid("55b3439e-7afc-427e-9d7e-b470063d4c04") },
                    { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), new Guid("586cea93-e806-4660-89da-2ddc892fd208") },
                    { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("59d041a9-50f4-47d6-b2c4-081f9cf73336") },
                    { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("692f2039-e00e-4657-94a3-d3db49d93015") },
                    { new Guid("754a24e0-2591-4910-a181-ce1af6716e77"), new Guid("7325fc9c-c587-4367-941f-18a12580e3bf") },
                    { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("77229aaf-e0b6-40c5-9f3a-166553ca4fdf") },
                    { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), new Guid("81868517-1a49-4f41-a296-0f26936b8f7e") },
                    { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("9861557a-7d5b-480b-b0b9-e077c066857b") },
                    { new Guid("a3f85702-9ad8-4d83-bdc0-8a4355fa3548"), new Guid("9bf7190d-22c6-4752-a93e-fc6e5bcf183a") },
                    { new Guid("754a24e0-2591-4910-a181-ce1af6716e77"), new Guid("b166874f-6799-475a-b322-cd12f226d547") },
                    { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("c6f3ab4f-de29-40d7-833f-fbf48e68de09") },
                    { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), new Guid("cf6ad59b-f991-4404-a3a3-6fe765b5f570") },
                    { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), new Guid("f533a0d0-eaeb-48ca-bc30-e348066dc4c3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cc17f01d-4acd-4b2b-8800-3ff8a9a912ce"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f4252c2b-a491-4104-aa76-1412b193811f"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("14836d91-3a5b-443a-8c4c-c69ead6bd09e") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), new Guid("16f91ae1-ad43-4741-bf1a-e75d57418dbd") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("66e3349d-f52a-47d0-a4f3-def5cbc871d6"), new Guid("226d0753-f1f2-45d1-9da7-2dedfa68c097") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), new Guid("290148e2-985e-4fe1-b59e-9d2e77401cdd") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("30940b9a-d383-457c-b14c-0d8823cedd36") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("46892596-7481-4727-9741-4ea753f02c80") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("53e2055b-3c93-40d9-b1aa-b78105ad9652") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a7b37a81-c4c7-416e-abc5-4539b9e3d0fb"), new Guid("55b3439e-7afc-427e-9d7e-b470063d4c04") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), new Guid("586cea93-e806-4660-89da-2ddc892fd208") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("59d041a9-50f4-47d6-b2c4-081f9cf73336") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"), new Guid("692f2039-e00e-4657-94a3-d3db49d93015") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("754a24e0-2591-4910-a181-ce1af6716e77"), new Guid("7325fc9c-c587-4367-941f-18a12580e3bf") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("77229aaf-e0b6-40c5-9f3a-166553ca4fdf") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("f6345228-d847-4baa-9414-eaad72219af1"), new Guid("81868517-1a49-4f41-a296-0f26936b8f7e") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("9861557a-7d5b-480b-b0b9-e077c066857b") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a3f85702-9ad8-4d83-bdc0-8a4355fa3548"), new Guid("9bf7190d-22c6-4752-a93e-fc6e5bcf183a") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("754a24e0-2591-4910-a181-ce1af6716e77"), new Guid("b166874f-6799-475a-b322-cd12f226d547") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("19611b52-5995-483b-836c-f18524dd69dc"), new Guid("c6f3ab4f-de29-40d7-833f-fbf48e68de09") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), new Guid("cf6ad59b-f991-4404-a3a3-6fe765b5f570") });

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"), new Guid("f533a0d0-eaeb-48ca-bc30-e348066dc4c3") });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("19611b52-5995-483b-836c-f18524dd69dc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("66e3349d-f52a-47d0-a4f3-def5cbc871d6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("754a24e0-2591-4910-a181-ce1af6716e77"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7bf4843d-4242-4cf7-b8d2-454ad3050dec"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a3f85702-9ad8-4d83-bdc0-8a4355fa3548"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a7b37a81-c4c7-416e-abc5-4539b9e3d0fb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e9583ba7-b2a4-41ce-a9a9-215a7ab9b67b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f6345228-d847-4baa-9414-eaad72219af1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("14836d91-3a5b-443a-8c4c-c69ead6bd09e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("16f91ae1-ad43-4741-bf1a-e75d57418dbd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("226d0753-f1f2-45d1-9da7-2dedfa68c097"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("290148e2-985e-4fe1-b59e-9d2e77401cdd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30940b9a-d383-457c-b14c-0d8823cedd36"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("46892596-7481-4727-9741-4ea753f02c80"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("53e2055b-3c93-40d9-b1aa-b78105ad9652"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("55b3439e-7afc-427e-9d7e-b470063d4c04"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("586cea93-e806-4660-89da-2ddc892fd208"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("59d041a9-50f4-47d6-b2c4-081f9cf73336"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("692f2039-e00e-4657-94a3-d3db49d93015"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7325fc9c-c587-4367-941f-18a12580e3bf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("77229aaf-e0b6-40c5-9f3a-166553ca4fdf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("81868517-1a49-4f41-a296-0f26936b8f7e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9861557a-7d5b-480b-b0b9-e077c066857b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9bf7190d-22c6-4752-a93e-fc6e5bcf183a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b166874f-6799-475a-b322-cd12f226d547"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c6f3ab4f-de29-40d7-833f-fbf48e68de09"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cf6ad59b-f991-4404-a3a3-6fe765b5f570"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f533a0d0-eaeb-48ca-bc30-e348066dc4c3"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "ImageFile");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), "Apparel for children", "Kids' Clothing" },
                    { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), "Belts, hats, scarves, bags", "Accessories" },
                    { new Guid("40d4a750-812b-4364-9027-676bea89216d"), "Apparel for men", "Men's Clothing" },
                    { new Guid("7c8006ab-4dbe-40b2-8876-08fa113cf986"), "Shoes, boots, sneakers", "Footwear" },
                    { new Guid("84d834cd-d647-4768-9350-94327748afcd"), "Underwear, pajamas, nightwear", "Underwear & Sleepwear" },
                    { new Guid("a5a9e340-9c8b-43c6-ac90-04162e2a8109"), "Activewear, gym clothes", "Sportswear" },
                    { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), "Apparel for women", "Women's Clothing" },
                    { new Guid("debcc794-3754-4e80-a031-5e771f2dfc27"), "Jackets, coats", "Outerwear" },
                    { new Guid("def16ffb-17bb-45b6-a192-e10143a6eaa4"), "Comfortable home clothing", "Loungewear" },
                    { new Guid("f2592271-553c-48b2-a8b9-38c751d3567e"), "Suits, dresses, gowns", "Formal Wear" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageFile", "IsAvailable", "Name", "Price", "Rating", "Sku", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0a1819eb-0f56-417b-aecb-08c038047248"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "100% cotton, comfortable fit", "mens-classic-tshirt.jpg", true, "Men's Classic T-Shirt", 25.99m, 4, "MEN-TSHIRT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("0c02c7af-5bcf-4166-8c9a-974e4e5222ec"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual and sporty", "womens-sneakers.jpg", true, "Sneakers for Women", 79.99m, 4, "WOM-SNEAK-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("15077ff2-a784-4746-9ba4-5f26cb28bf11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Durable and spacious", "backpack.jpg", true, "Backpack", 49.99m, 4, "ACC-BAG-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("21853cd8-c563-4266-a0a3-80576ccbd164"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stretchable and comfortable", "womens-yoga-pants.jpg", true, "Women's Yoga Pants", 45.00m, 4, "WOM-YOGA-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2d787418-ff8f-4d6d-b29d-3d2b7c062ecf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight, floral print", "womens-summer-dress.jpg", true, "Women's Summer Dress", 39.99m, 5, "WOM-DRESS-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("378690c5-295d-4955-8860-abbed9c2c9ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breathable material, adjustable", "sports-cap.jpg", true, "Sports Cap", 14.99m, 4, "ACC-CAP-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4d03f9af-1097-414a-b704-213a95e36b72"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft cotton top and pants", "loungewear-set.jpg", true, "Loungewear Set", 59.99m, 4, "LOUNGE-SET-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("540a8135-cbd8-4bb3-858d-a3347cc9d16d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight and durable", "kids-sneakers.jpg", true, "Kids' Sneakers", 34.99m, 5, "KID-SNEAK-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7293599c-6113-4303-b1c9-01c81e762166"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable nightwear", "mens-pajama-set.jpg", true, "Men's Pajama Set", 39.99m, 4, "UNDER-MEN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("796073c9-87e2-41b6-b742-f58c4ce8311f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight with cushioned sole", "mens-running-shoes.jpg", true, "Men's Running Shoes", 89.99m, 5, "MEN-RUN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7c7a8e8e-6926-422a-9e8c-232932e4d47d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Denim, slim fit, dark wash", "mens-slim-jeans.jpg", true, "Men's Slim Jeans", 49.99m, 5, "MEN-JEANS-02", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("936f8bec-3b59-4b87-a315-a5914c3c9af7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof and insulated", "winter-jacket.jpg", true, "Winter Jacket", 129.99m, 5, "OUT-JACKET-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9fe80821-3cf3-48f4-b674-1a8079beea59"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tailored fit, premium fabric", "mens-suit.jpg", true, "Men's Suit", 249.99m, 5, "FORM-SUIT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a2b9d992-e3e4-4e9b-8f2a-1ceff976d5ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Genuine leather, classic buckle", "leather-belt.jpg", true, "Leather Belt", 19.99m, 4, "ACC-BELT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a9278579-5a6e-4b5d-9693-78eca5817d05"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft cotton, colorful design", "kids-hoodie.jpg", true, "Kids' Hoodie", 29.99m, 4, "KID-HOODIE-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b31becfe-cd73-42dd-98be-171c20f672a7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof and insulated", "kids-winter-boots.jpg", true, "Kids' Winter Boots", 59.99m, 5, "KID-BOOT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d65d91ae-99e4-48c4-a77a-cd495cb9761f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warm and stylish", "mens-hoodie.jpg", true, "Men's Hoodie", 59.99m, 4, "MEN-HOODIE-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("dcb09d99-4e56-4bfe-bbb9-34fa8f67c56c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft and cozy sleepwear", "womens-pajama-set.jpg", true, "Women's Pajama Set", 42.99m, 5, "UNDER-WOM-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e66d1bec-0a22-47a5-8b3e-ce7eadc94863"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elegant floor-length gown", "womens-evening-gown.jpg", true, "Women's Evening Gown", 199.99m, 5, "FORM-GOWN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f53ec6d4-0927-4b42-8799-b7824549c53d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silk blend, long sleeves", "womens-blouse.jpg", true, "Women's Blouse", 35.50m, 4, "WOM-BLOUSE-02", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("0a1819eb-0f56-417b-aecb-08c038047248") },
                    { new Guid("7c8006ab-4dbe-40b2-8876-08fa113cf986"), new Guid("0c02c7af-5bcf-4166-8c9a-974e4e5222ec") },
                    { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), new Guid("15077ff2-a784-4746-9ba4-5f26cb28bf11") },
                    { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("21853cd8-c563-4266-a0a3-80576ccbd164") },
                    { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("2d787418-ff8f-4d6d-b29d-3d2b7c062ecf") },
                    { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), new Guid("378690c5-295d-4955-8860-abbed9c2c9ab") },
                    { new Guid("def16ffb-17bb-45b6-a192-e10143a6eaa4"), new Guid("4d03f9af-1097-414a-b704-213a95e36b72") },
                    { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), new Guid("540a8135-cbd8-4bb3-858d-a3347cc9d16d") },
                    { new Guid("84d834cd-d647-4768-9350-94327748afcd"), new Guid("7293599c-6113-4303-b1c9-01c81e762166") },
                    { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("796073c9-87e2-41b6-b742-f58c4ce8311f") },
                    { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("7c7a8e8e-6926-422a-9e8c-232932e4d47d") },
                    { new Guid("debcc794-3754-4e80-a031-5e771f2dfc27"), new Guid("936f8bec-3b59-4b87-a315-a5914c3c9af7") },
                    { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("9fe80821-3cf3-48f4-b674-1a8079beea59") },
                    { new Guid("3d87cbf9-e79c-4833-8374-84b0858925c8"), new Guid("a2b9d992-e3e4-4e9b-8f2a-1ceff976d5ab") },
                    { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), new Guid("a9278579-5a6e-4b5d-9693-78eca5817d05") },
                    { new Guid("3c7d953e-61fa-47cb-b263-30c55f364f7d"), new Guid("b31becfe-cd73-42dd-98be-171c20f672a7") },
                    { new Guid("40d4a750-812b-4364-9027-676bea89216d"), new Guid("d65d91ae-99e4-48c4-a77a-cd495cb9761f") },
                    { new Guid("84d834cd-d647-4768-9350-94327748afcd"), new Guid("dcb09d99-4e56-4bfe-bbb9-34fa8f67c56c") },
                    { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("e66d1bec-0a22-47a5-8b3e-ce7eadc94863") },
                    { new Guid("ca41a2c8-bd56-4c12-9efb-944109a7d404"), new Guid("f53ec6d4-0927-4b42-8799-b7824549c53d") }
                });
        }
    }
}
