using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    ImageFile = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
