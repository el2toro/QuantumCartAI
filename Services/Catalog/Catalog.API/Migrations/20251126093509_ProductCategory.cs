using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class ProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("013739f0-327d-4a1d-b044-b2e3c9ffbe52"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1542099c-01fc-4cc5-9353-7a337b5dde2b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1d7e3d34-d0a7-406a-b0e1-5957294951a7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("24dd06cd-d25d-4fb7-81de-bbcfcd71796e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("30b9e589-2f04-438e-bed8-890bea98da11"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("43b45b3c-11e3-480c-978c-566f3d13f190"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("53e3c819-3e9b-4a69-856c-e8e67f61b90e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("676794d3-a51e-40ef-a8d7-a51f59cac341"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7838671a-7933-4074-a72e-d3e22720b940"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d6c56b3-0d4a-47b6-b278-91364e267ad5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("87ac07a4-ab43-4375-888b-4c5016e44c3b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8b159607-19dd-4668-8e60-5c0dae465ef9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bab7ec60-1090-492b-aaa8-e0a68a4b0d9a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c77705e7-4b21-4944-86d2-9ae861d35637"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d5375352-fabb-4a8a-b341-76b4de6d3a46"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ded587bf-ffd7-4b26-8548-fea3a8bd931b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e87c0474-3f3e-4ae9-9ffe-ecf209659724"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f15ea535-5296-44e0-9bc4-657d9309d860"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f9602190-2b29-48da-8c1b-0ca40595104d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fee83c9a-bca2-4523-9596-6e652c49e387"));

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                columns: new[] { "ProductId", "CategoryId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

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

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Apparel and accessories for men", "Men" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Apparel and accessories for women", "Women" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Clothing, shoes, and accessories for children", "Kids" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Shoes, boots, sneakers, and sandals", "Footwear" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Bags, belts, hats, jewelry, and more", "Accessories" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Athletic wear, gym apparel, and sports accessories", "Sportswear" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Jackets, coats, and seasonal outerwear", "Outerwear" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Suits, dresses, and other formal clothing", "Formal Wear" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Casual and comfortable clothing for home", "Loungewear" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Undergarments, pajamas, and nightwear", "Underwear & Sleepwear" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageFile", "IsAvailable", "Name", "Price", "Rating", "Sku", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("013739f0-327d-4a1d-b044-b2e3c9ffbe52"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual and sporty", "images/products/womens-sneakers.jpg", true, "Sneakers for Women", 79.99m, 4, "WOM-SNEAK-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1542099c-01fc-4cc5-9353-7a337b5dde2b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft cotton, colorful design", "images/products/kids-hoodie.jpg", true, "Kids' Hoodie", 29.99m, 4, "KID-HOODIE-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1d7e3d34-d0a7-406a-b0e1-5957294951a7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable nightwear", "images/products/mens-pajama-set.jpg", true, "Men's Pajama Set", 39.99m, 4, "UNDER-MEN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("24dd06cd-d25d-4fb7-81de-bbcfcd71796e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warm and stylish", "images/products/mens-hoodie.jpg", true, "Men's Hoodie", 59.99m, 4, "MEN-HOODIE-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("30b9e589-2f04-438e-bed8-890bea98da11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stretchable and comfortable", "images/products/womens-yoga-pants.jpg", true, "Women's Yoga Pants", 45.00m, 4, "WOM-YOGA-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("43b45b3c-11e3-480c-978c-566f3d13f190"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Durable and spacious", "images/products/backpack.jpg", true, "Backpack", 49.99m, 4, "ACC-BAG-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("53e3c819-3e9b-4a69-856c-e8e67f61b90e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Genuine leather, classic buckle", "images/products/leather-belt.jpg", true, "Leather Belt", 19.99m, 4, "ACC-BELT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("676794d3-a51e-40ef-a8d7-a51f59cac341"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight, floral print", "images/products/womens-summer-dress.jpg", true, "Women's Summer Dress", 39.99m, 5, "WOM-DRESS-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7838671a-7933-4074-a72e-d3e22720b940"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight and durable", "images/products/kids-sneakers.jpg", true, "Kids' Sneakers", 34.99m, 5, "KID-SNEAK-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7d6c56b3-0d4a-47b6-b278-91364e267ad5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof and insulated", "images/products/winter-jacket.jpg", true, "Winter Jacket", 129.99m, 5, "OUT-JACKET-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("87ac07a4-ab43-4375-888b-4c5016e44c3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breathable material, adjustable", "images/products/sports-cap.jpg", true, "Sports Cap", 14.99m, 4, "ACC-CAP-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8b159607-19dd-4668-8e60-5c0dae465ef9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "100% cotton, comfortable fit", "images/products/mens-classic-tshirt.jpg", true, "Men's Classic T-Shirt", 25.99m, 4, "MEN-TSHIRT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bab7ec60-1090-492b-aaa8-e0a68a4b0d9a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tailored fit, premium fabric", "images/products/mens-suit.jpg", true, "Men's Suit", 249.99m, 5, "FORM-SUIT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c77705e7-4b21-4944-86d2-9ae861d35637"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight with cushioned sole", "images/products/mens-running-shoes.jpg", true, "Men's Running Shoes", 89.99m, 5, "MEN-RUN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d5375352-fabb-4a8a-b341-76b4de6d3a46"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft cotton top and pants", "images/products/loungewear-set.jpg", true, "Loungewear Set", 59.99m, 4, "LOUNGE-SET-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ded587bf-ffd7-4b26-8548-fea3a8bd931b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof and insulated", "images/products/kids-winter-boots.jpg", true, "Kids' Winter Boots", 59.99m, 5, "KID-BOOT-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e87c0474-3f3e-4ae9-9ffe-ecf209659724"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elegant floor-length gown", "images/products/womens-evening-gown.jpg", true, "Women's Evening Gown", 199.99m, 5, "FORM-GOWN-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f15ea535-5296-44e0-9bc4-657d9309d860"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Denim, slim fit, dark wash", "images/products/mens-slim-jeans.jpg", true, "Men's Slim Jeans", 49.99m, 5, "MEN-JEANS-02", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f9602190-2b29-48da-8c1b-0ca40595104d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silk blend, long sleeves", "images/products/womens-blouse.jpg", true, "Women's Blouse", 35.50m, 4, "WOM-BLOUSE-02", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fee83c9a-bca2-4523-9596-6e652c49e387"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soft and cozy sleepwear", "images/products/womens-pajama-set.jpg", true, "Women's Pajama Set", 42.99m, 5, "UNDER-WOM-01", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
