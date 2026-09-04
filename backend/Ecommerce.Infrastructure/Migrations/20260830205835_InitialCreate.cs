using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    unit_number = table.Column<string>(type: "text", nullable: false),
                    street_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_line2 = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    parent_category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_category_id",
                        column: x => x.parent_category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    brand_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carts", x => x.id);
                    table.ForeignKey(
                        name: "fk_carts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    order_status = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipping_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    provider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    card_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_methods", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_methods_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_address_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_address_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_colors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_name = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_colors", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_colors_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    image_text = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_sizes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    size_value = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sizes", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_sizes_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    review_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    review_text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipment_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipments", x => x.id);
                    table.ForeignKey(
                        name: "fk_shipments_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shipments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    payment_method_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    payment_status = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "address_line1", "address_line2", "city", "country", "created_at", "postal_code", "street_number", "unit_number" },
                values: new object[,]
                {
                    { new Guid("37d50533-5d16-45eb-92cb-a63d37780f0d"), "Terveystie", "", "Houston", "USA", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7460), "77001", "101 Pine St", "1452" },
                    { new Guid("7efc2dce-45e9-4e81-942d-52016c422256"), "Terveystie", "", "Miami", "USA", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7460), "33101", "202 Cedar St", "1452" },
                    { new Guid("9e50de9d-f57f-4fc5-be11-68036f402a91"), "Terveystie", "", "Los Angeles", "USA", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7450), "90001", "456 Elm St", "1452" },
                    { new Guid("cbc715b3-fcfb-4257-970e-c96321ae37c2"), "Terveystie", "", "Chicago", "USA", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7450), "60601", "789 Oak St", "1452" },
                    { new Guid("ce1782ed-9a6b-4877-bb47-b0651b9f0627"), "Terveystie", "", "New York", "USA", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(6880), "10001", "123 Main St", "1452" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_name", "created_at", "parent_category_id" },
                values: new object[,]
                {
                    { new Guid("8c1c5222-7793-4f00-ad1b-ab5bf4b37e70"), "Women", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7750), null },
                    { new Guid("d86a0e53-cec3-4011-86b4-7fbbdb91f1f0"), "Kids", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7750), null },
                    { new Guid("eaca11f9-c60b-4c40-98be-c240b6784f02"), "Men", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7660), null }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password_hash", "phone_number", "role", "salt" },
                values: new object[,]
                {
                    { new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92"), new DateTime(2026, 8, 30, 20, 58, 35, 248, DateTimeKind.Utc).AddTicks(2400), "email3@example.com", "Bob", "Johnson", "Qj4GAGK7nNQGp7mSGl75CQZ7CTahshytOvAGYhzLi6I=", "0413333333", 2, new byte[] { 219, 132, 133, 232, 247, 209, 141, 60, 110, 104, 21, 118, 48, 237, 105, 200 } },
                    { new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d"), new DateTime(2026, 8, 30, 20, 58, 35, 251, DateTimeKind.Utc).AddTicks(2020), "email5@example.com", "Charlie", "Brown", "mALwklIKEg7DxnxQu1FkS8HZzHB/jLjouISrOCpXJoE=", "0413333333", 2, new byte[] { 27, 61, 225, 47, 142, 55, 179, 184, 204, 68, 226, 127, 175, 71, 193, 56 } },
                    { new Guid("56fce91c-366e-43dc-9511-075477992eed"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(6710), "email6@example.com", "Oula", "Mok", "Ujk81/Dmtn5JTl3Oz3S8kHw5kgjv6U6FmLwRMIz4/k4=", "0413333333", 2, new byte[] { 244, 38, 92, 165, 87, 26, 173, 52, 206, 8, 54, 214, 177, 192, 179, 28 } },
                    { new Guid("871cfaa6-409a-473f-b20a-168993c1990b"), new DateTime(2026, 8, 30, 20, 58, 35, 249, DateTimeKind.Utc).AddTicks(7370), "email4@example.com", "Alice", "Williams", "4YVheVPlf46R3VRgGrsMPCumkaJpyIapawlYuCmprnY=", "0413333333", 2, new byte[] { 101, 187, 39, 19, 226, 225, 189, 30, 171, 241, 126, 253, 32, 19, 239, 63 } },
                    { new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb"), new DateTime(2026, 8, 30, 20, 58, 35, 245, DateTimeKind.Utc).AddTicks(1460), "email1@example.com", "Moh", "nach", "B3/leZZ8Gd81cLo/3npsdDq9fPxX7zLU6jPOyMC/8xI=", "0413333333", 1, new byte[] { 220, 125, 205, 71, 165, 216, 195, 103, 105, 207, 191, 245, 57, 15, 30, 247 } },
                    { new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400"), new DateTime(2026, 8, 30, 20, 58, 35, 246, DateTimeKind.Utc).AddTicks(7520), "email2@example.com", "Jane", "Smith", "zOReeXQkwiMXdGpn0vlV/D3PvcRVuIxmHdjhW5aV7Ag=", "0413333333", 2, new byte[] { 219, 180, 152, 23, 125, 36, 105, 132, 175, 59, 40, 150, 44, 173, 149, 178 } }
                });

            migrationBuilder.InsertData(
                table: "carts",
                columns: new[] { "id", "created_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("3500a943-9e1b-4033-b7cf-e862f8173953"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8890), new Guid("871cfaa6-409a-473f-b20a-168993c1990b") },
                    { new Guid("70351566-cadd-4f36-9b71-f53d0483b942"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8780), new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("72093732-d683-49ab-98d9-09ac4bc3f325"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8890), new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d") },
                    { new Guid("86c2cc32-1085-4fb2-90b3-0e33efc402a0"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8880), new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400") },
                    { new Guid("d325af8c-fede-4f03-8757-00d837680bab"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8880), new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92") }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_name", "created_at", "parent_category_id" },
                values: new object[,]
                {
                    { new Guid("09f1aa42-b59e-48ed-b11b-c0999070ec34"), "MaternityWear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7990), new Guid("8c1c5222-7793-4f00-ad1b-ab5bf4b37e70") },
                    { new Guid("1286e4f4-4661-4ec1-9364-261b113fd887"), "Footwear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7980), new Guid("eaca11f9-c60b-4c40-98be-c240b6784f02") },
                    { new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), "Bottomwear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7970), new Guid("eaca11f9-c60b-4c40-98be-c240b6784f02") },
                    { new Guid("3a085596-1c2b-4564-afc1-85dde5acd394"), "Topwear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7750), new Guid("eaca11f9-c60b-4c40-98be-c240b6784f02") },
                    { new Guid("7f6d2a5d-8ca9-4e53-9f4b-e58a605ef84d"), "School Supplies", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8000), new Guid("d86a0e53-cec3-4011-86b4-7fbbdb91f1f0") },
                    { new Guid("8987dee6-199d-4d52-8812-a79cc77230fd"), "Toys", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8000), new Guid("d86a0e53-cec3-4011-86b4-7fbbdb91f1f0") },
                    { new Guid("909c381b-6da4-48f9-8aaf-37f4c16a3fda"), "EthnicWear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7990), new Guid("8c1c5222-7793-4f00-ad1b-ab5bf4b37e70") },
                    { new Guid("9a4ef507-1fe8-45c4-9f86-8549e21b7e32"), "Footwear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7990), new Guid("8c1c5222-7793-4f00-ad1b-ab5bf4b37e70") },
                    { new Guid("d5d1c503-f29b-4736-acce-5cabfdcb29b5"), "Dresses", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7980), new Guid("8c1c5222-7793-4f00-ad1b-ab5bf4b37e70") },
                    { new Guid("e724f017-504c-49df-8920-025d05c6a71f"), "WinterWear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(7980), new Guid("eaca11f9-c60b-4c40-98be-c240b6784f02") },
                    { new Guid("f58c5228-09d2-4b7f-882e-f4e50e6e00d3"), "Footwear", new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8000), new Guid("d86a0e53-cec3-4011-86b4-7fbbdb91f1f0") }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "address_id", "created_at", "order_date", "order_status", "shipping_address_id", "total_price", "user_id" },
                values: new object[,]
                {
                    { new Guid("219c727f-42fe-4d61-a53f-9f04c349ca7a"), null, new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(9640), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(9900), 0, new Guid("ce1782ed-9a6b-4877-bb47-b0651b9f0627"), 340m, new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("578062d1-60dd-4ed2-ab45-5519b740886b"), null, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(70), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(70), 2, new Guid("cbc715b3-fcfb-4257-970e-c96321ae37c2"), 336m, new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92") },
                    { new Guid("7ce44b11-cddf-4613-8aea-35e7b04b831f"), null, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(60), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(70), 1, new Guid("9e50de9d-f57f-4fc5-be11-68036f402a91"), 50m, new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400") },
                    { new Guid("c6c42647-2646-4ee7-b01f-1b0e4e77c788"), null, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(70), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(80), 0, new Guid("7efc2dce-45e9-4e81-942d-52016c422256"), 11m, new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d") },
                    { new Guid("d8406c21-a6ed-45ac-acb1-4d7787c0c42b"), null, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(70), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(70), 3, new Guid("37d50533-5d16-45eb-92cb-a63d37780f0d"), 65m, new Guid("871cfaa6-409a-473f-b20a-168993c1990b") }
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id", "card_number", "created_at", "expiry_date", "payment_type", "provider", "user_id" },
                values: new object[,]
                {
                    { new Guid("01552634-ee17-40b7-99c9-b2015ffba4ff"), "1151184", new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(740), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(980), "CreditCard", "Visa", new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("2b3f2b8e-7e85-49e5-86c4-a83a833b6270"), "14528795", new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1150), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1150), "BankTransfer", "Bank", new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d") },
                    { new Guid("6c6877a1-458a-444a-9de2-c010dac785d7"), "14528795", new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1150), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1150), "CreditCard", "MasterCard", new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92") },
                    { new Guid("7cb12ee3-899e-4e40-b8a3-d9c54c6b639e"), "14528795", new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1140), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1140), "CreditCard", "Visa", new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400") },
                    { new Guid("932cad5c-f8d7-42f2-b265-86a2e4f7e45e"), "14528795", new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1150), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1150), "PayPal", "PayPal", new Guid("871cfaa6-409a-473f-b20a-168993c1990b") }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_name", "category_id", "created_at", "description", "price", "product_title", "quantity" },
                values: new object[,]
                {
                    { new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), "Adibas", new Guid("8c1c5222-7793-4f00-ad1b-ab5bf4b37e70"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8650), "Description", 699.99m, "Smartphone ABC", 3 },
                    { new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), "Adibas", new Guid("eaca11f9-c60b-4c40-98be-c240b6784f02"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8140), "Description", 999.99m, "Laptop XYZ", 3 },
                    { new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), "Adibas", new Guid("d86a0e53-cec3-4011-86b4-7fbbdb91f1f0"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8660), "Description", 499.99m, "TV DEF", 3 }
                });

            migrationBuilder.InsertData(
                table: "user_address",
                columns: new[] { "id", "address_id", "created_at", "is_default", "user_id" },
                values: new object[,]
                {
                    { new Guid("022cff49-e578-4653-a9e6-5184fbbc89ab"), new Guid("9e50de9d-f57f-4fc5-be11-68036f402a91"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(4320), true, new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92") },
                    { new Guid("3d897d13-27c1-4fa8-9a26-c5dcc5e8fc5d"), new Guid("cbc715b3-fcfb-4257-970e-c96321ae37c2"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(4320), false, new Guid("871cfaa6-409a-473f-b20a-168993c1990b") },
                    { new Guid("9b19c6d8-83f2-4176-a406-96476c2bcb3e"), new Guid("ce1782ed-9a6b-4877-bb47-b0651b9f0627"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(4060), true, new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("a204c9d4-0060-42ad-bf77-50266ced65c7"), new Guid("ce1782ed-9a6b-4877-bb47-b0651b9f0627"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(4320), false, new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400") },
                    { new Guid("f43de107-992d-42c9-bd61-bc60da9bb73a"), new Guid("37d50533-5d16-45eb-92cb-a63d37780f0d"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(4330), true, new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d") }
                });

            migrationBuilder.InsertData(
                table: "cart_items",
                columns: new[] { "id", "cart_id", "created_at", "product_id", "quantity", "total_price", "unit_price" },
                values: new object[,]
                {
                    { new Guid("c057e682-6016-4c26-8a25-9f0f16759165"), new Guid("86c2cc32-1085-4fb2-90b3-0e33efc402a0"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(9400), new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), 1, 19.9m, 19.9m },
                    { new Guid("db472ac3-8d03-4227-ac6f-5cf1000550fe"), new Guid("d325af8c-fede-4f03-8757-00d837680bab"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(9400), new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), 3, 59.7m, 19.9m },
                    { new Guid("e1e91b77-c8d0-4155-8dd2-645e418f6fa6"), new Guid("70351566-cadd-4f36-9b71-f53d0483b942"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8970), new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), 2, 39.8m, 19.9m }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "id", "created_at", "order_id", "price", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("8bd84e8c-f999-4c7c-938c-5a7d8f33a29e"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(550), new Guid("d8406c21-a6ed-45ac-acb1-4d7787c0c42b"), 5m, new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), 3 },
                    { new Guid("991398f2-7a70-4e25-8f71-4ad7fd28f706"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(530), new Guid("219c727f-42fe-4d61-a53f-9f04c349ca7a"), 15m, new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), 1 },
                    { new Guid("a692ce48-2cef-4436-83c7-60568f6e06b5"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(530), new Guid("7ce44b11-cddf-4613-8aea-35e7b04b831f"), 5m, new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), 3 },
                    { new Guid("bfd3274c-d7db-4357-8641-c868fd905c4d"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(550), new Guid("d8406c21-a6ed-45ac-acb1-4d7787c0c42b"), 45m, new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), 1 },
                    { new Guid("f6c56950-810c-4d2c-b8f2-840a05d2734f"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(540), new Guid("7ce44b11-cddf-4613-8aea-35e7b04b831f"), 45m, new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), 1 },
                    { new Guid("fb18b4ed-9924-4e31-a2ac-1c13ec0d6475"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(550), new Guid("d8406c21-a6ed-45ac-acb1-4d7787c0c42b"), 15m, new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), 1 }
                });

            migrationBuilder.InsertData(
                table: "payments",
                columns: new[] { "id", "amount", "created_at", "order_id", "payment_date", "payment_method_id", "payment_status", "user_id" },
                values: new object[,]
                {
                    { new Guid("51751ad2-feb8-40ff-9082-920c57c2cf1a"), 89.99m, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1730), new Guid("7ce44b11-cddf-4613-8aea-35e7b04b831f"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1730), new Guid("01552634-ee17-40b7-99c9-b2015ffba4ff"), 1, new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("5dba705a-e2be-4b46-bf68-15bad8e2c8e5"), 299.99m, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1240), new Guid("c6c42647-2646-4ee7-b01f-1b0e4e77c788"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1490), new Guid("01552634-ee17-40b7-99c9-b2015ffba4ff"), 1, new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("6f930b34-904e-48ad-8469-706808064406"), 399.99m, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1730), new Guid("d8406c21-a6ed-45ac-acb1-4d7787c0c42b"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1740), new Guid("01552634-ee17-40b7-99c9-b2015ffba4ff"), 1, new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d") },
                    { new Guid("987ce993-ab33-43cd-97cd-bb88a718aad0"), 149.99m, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1720), new Guid("219c727f-42fe-4d61-a53f-9f04c349ca7a"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1730), new Guid("01552634-ee17-40b7-99c9-b2015ffba4ff"), 1, new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400") },
                    { new Guid("e368a341-45a6-43d0-b6b9-0558ea507057"), 49.99m, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1730), new Guid("578062d1-60dd-4ed2-ab45-5519b740886b"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1730), new Guid("01552634-ee17-40b7-99c9-b2015ffba4ff"), 1, new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92") }
                });

            migrationBuilder.InsertData(
                table: "product_colors",
                columns: new[] { "id", "color_name", "created_at", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("17c8b247-6700-48c4-8c54-efae122ca52a"), 2, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2400), new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), 15 },
                    { new Guid("3e5753db-a027-49b1-b701-a563a1365ead"), 1, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2640), new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), 5 },
                    { new Guid("dd6ba5d0-651f-49da-8da2-9c80dea4836b"), 5, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2640), new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), 3 }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "id", "created_at", "image_text", "image_url", "is_default", "product_id" },
                values: new object[,]
                {
                    { new Guid("074134c8-880a-4382-ae35-441812f4c49e"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2300), "Sample product image", "https://picsum.photos/200/300", true, new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203") },
                    { new Guid("0bb78710-e8ed-4ad8-94a1-fb894ab4d80b"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(1850), "Sample product image", "https://picsum.photos/200/300", true, new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173") },
                    { new Guid("b13f2823-dca6-42aa-8e47-514fe30fe41e"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2300), "Sample product image", "https://picsum.photos/200/300", false, new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173") },
                    { new Guid("b7df2fe9-985e-40ed-9af2-a3fd8556eaaf"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2300), "Sample product image", "https://picsum.photos/200/300", false, new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173") },
                    { new Guid("e9c233e3-95c4-4c23-8571-f7a73007936a"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2310), "Sample product image", "https://picsum.photos/200/300", true, new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471") },
                    { new Guid("fad25ecb-90d3-4c13-ba72-bbd27f190757"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2310), "Sample product image", "https://picsum.photos/200/300", false, new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471") }
                });

            migrationBuilder.InsertData(
                table: "product_sizes",
                columns: new[] { "id", "created_at", "product_id", "quantity", "size_value" },
                values: new object[,]
                {
                    { new Guid("4331a72e-89bd-4c4a-b266-c7c963d8a8de"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2980), new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), 10, 3 },
                    { new Guid("865364c5-1e13-4e9c-b653-996186655526"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2740), new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), 5, 2 },
                    { new Guid("b2ed4bd0-b6cf-46a6-b0ae-9a28f49f107f"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2980), new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_name", "category_id", "created_at", "description", "price", "product_title", "quantity" },
                values: new object[,]
                {
                    { new Guid("1429afd1-0743-4dc1-a7ba-5d208ef64ab7"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8690), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("2f6c0349-c516-488f-9168-d4cce65f0f46"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8670), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("32e7f92e-53ce-4306-94a4-309cd9e625d2"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8690), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), "Adibas", new Guid("3a085596-1c2b-4564-afc1-85dde5acd394"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8660), "Description", 299.99m, "Tablet GHI", 3 },
                    { new Guid("525a464c-5753-407e-9570-e39881afe8b0"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8690), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8660), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("5c9f0ea9-8175-40e1-ae2a-0d7ddd3c570a"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8680), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("5e849962-51f8-4773-9836-a9f70c4b2be3"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8680), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("64fe05b4-49f3-4fcf-8c89-893089e5ec23"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8670), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("af15f574-2d5b-4073-88a1-2a6af7b7c136"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8670), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("e097e0fc-b7e5-4352-b4c4-17fae85ed4c4"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8670), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("f4696c53-31ae-4d66-9f38-0c0bb75c05e7"), "Adibas", new Guid("132b2a1a-13a5-4736-a88b-380dcf09e790"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(8680), "Description", 149.99m, "Headphones JKL", 3 }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "id", "created_at", "product_id", "rating", "review_date", "review_text", "user_id" },
                values: new object[,]
                {
                    { new Guid("4a7a0472-6a9d-4f70-ab0d-8d9a01393a09"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3090), new Guid("44b6d4d6-fe4f-4204-9d2a-96c2d9214173"), 5, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3410), "Excellent!", new Guid("a56b9529-61f5-4981-8719-a45d8d9e48fb") },
                    { new Guid("71ada713-8a99-48ea-8b7c-6ef65ce30086"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3490), new Guid("3bbcf462-2fae-4550-9cfd-7be95c0b5471"), 4, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3500), "Very good.", new Guid("ef42401b-3c4a-455d-b0a6-c99bbb3fa400") },
                    { new Guid("d93d75c1-2d5d-4b3a-84fb-814a44843b4f"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3500), new Guid("835591b1-aafe-4084-b2be-e4d4c2f16203"), 3, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3500), "Average.", new Guid("023150e8-0656-4c9e-84ec-7d583bb17d92") }
                });

            migrationBuilder.InsertData(
                table: "shipments",
                columns: new[] { "id", "address_id", "created_at", "order_id", "shipment_date", "shipment_status" },
                values: new object[,]
                {
                    { new Guid("494e2a9a-f2d7-481f-825b-0db82d3be618"), new Guid("cbc715b3-fcfb-4257-970e-c96321ae37c2"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), new Guid("578062d1-60dd-4ed2-ab45-5519b740886b"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), 2 },
                    { new Guid("8732fe0a-406a-4644-ac52-d9f77bb643bf"), new Guid("9e50de9d-f57f-4fc5-be11-68036f402a91"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3950), new Guid("7ce44b11-cddf-4613-8aea-35e7b04b831f"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), 4 },
                    { new Guid("9acae167-f9dd-474e-8cb0-e0ca80484b48"), new Guid("7efc2dce-45e9-4e81-942d-52016c422256"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), new Guid("c6c42647-2646-4ee7-b01f-1b0e4e77c788"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), 1 },
                    { new Guid("ad02a56e-7e92-491b-8bfc-e247fa8559a0"), new Guid("ce1782ed-9a6b-4877-bb47-b0651b9f0627"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3620), new Guid("219c727f-42fe-4d61-a53f-9f04c349ca7a"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3710), 0 },
                    { new Guid("cb88c70f-f643-4f3d-b452-7c1e86236483"), new Guid("37d50533-5d16-45eb-92cb-a63d37780f0d"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), new Guid("d8406c21-a6ed-45ac-acb1-4d7787c0c42b"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3960), 3 }
                });

            migrationBuilder.InsertData(
                table: "cart_items",
                columns: new[] { "id", "cart_id", "created_at", "product_id", "quantity", "total_price", "unit_price" },
                values: new object[,]
                {
                    { new Guid("0cce292d-2262-4c46-b1fd-9d178fa811d4"), new Guid("3500a943-9e1b-4033-b7cf-e862f8173953"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(9400), new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), 1, 19.9m, 19.9m },
                    { new Guid("dbee6917-f8fa-4cf6-87c3-7cac0c89e47d"), new Guid("72093732-d683-49ab-98d9-09ac4bc3f325"), new DateTime(2026, 8, 30, 20, 58, 35, 252, DateTimeKind.Utc).AddTicks(9410), new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), 4, 79.6m, 19.9m }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "id", "created_at", "order_id", "price", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("46e0f680-ec2a-4237-a01f-7a4b63347367"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(560), new Guid("c6c42647-2646-4ee7-b01f-1b0e4e77c788"), 11m, new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), 4 },
                    { new Guid("a48e23e8-81ae-455b-abbb-440229476dcc"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(160), new Guid("219c727f-42fe-4d61-a53f-9f04c349ca7a"), 325m, new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), 2 },
                    { new Guid("b97d5896-e65a-40e7-aeb7-1e7058175cc4"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(540), new Guid("578062d1-60dd-4ed2-ab45-5519b740886b"), 11m, new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), 4 },
                    { new Guid("e7c04134-1405-4554-9ca4-0ad2e032103d"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(540), new Guid("578062d1-60dd-4ed2-ab45-5519b740886b"), 325m, new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), 2 }
                });

            migrationBuilder.InsertData(
                table: "product_colors",
                columns: new[] { "id", "color_name", "created_at", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("2a68ec60-a80b-4ddc-a009-9bcb0d5fa1f6"), 3, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2650), new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), 4 },
                    { new Guid("e8419348-8815-4a60-b37d-ffc918182d5a"), 0, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2640), new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), 6 }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "id", "created_at", "image_text", "image_url", "is_default", "product_id" },
                values: new object[,]
                {
                    { new Guid("8f8dcbc7-5835-4a1e-abe8-a83aecb3416a"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2310), "Sample product image", "https://picsum.photos/200/300", false, new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4") },
                    { new Guid("97ef29bc-cecd-4242-9c50-c6e035240fe0"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2300), "Sample product image", "https://picsum.photos/200/300", true, new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4") },
                    { new Guid("df65d5a6-4d22-4e69-8ce5-1355cc94fb54"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2290), "Sample product image", "https://picsum.photos/200/300", true, new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb") }
                });

            migrationBuilder.InsertData(
                table: "product_sizes",
                columns: new[] { "id", "created_at", "product_id", "quantity", "size_value" },
                values: new object[,]
                {
                    { new Guid("62d1e514-e0e3-4b7f-aee8-f3a8a99c962e"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2990), new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), 14, 1 },
                    { new Guid("9a720452-0579-4291-a438-382d95c5c5c5"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(2990), new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), 9, 0 }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "id", "created_at", "product_id", "rating", "review_date", "review_text", "user_id" },
                values: new object[,]
                {
                    { new Guid("5f0e1b08-6114-40fb-9cb4-05544eac5db8"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3500), new Guid("558a900d-3d12-47c3-afff-3e7a0a424df4"), 1, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3510), "Terrible!", new Guid("50c209dc-9fbd-49d6-bffe-b3d9177c6d7d") },
                    { new Guid("88f3b9c3-2798-4d5f-b4e6-1388da5cee4a"), new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3500), new Guid("4b480c84-c116-4e05-8e71-5d3bf98bddbb"), 2, new DateTime(2026, 8, 30, 20, 58, 35, 253, DateTimeKind.Utc).AddTicks(3500), "Not great.", new Guid("871cfaa6-409a-473f-b20a-168993c1990b") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_product_id",
                table: "cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_carts_user_id",
                table: "carts",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_category_id",
                table: "categories",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_methods_user_id",
                table: "payment_methods",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_order_id",
                table: "payments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_payment_method_id",
                table: "payments",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_user_id",
                table: "payments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_colors_product_id",
                table: "product_colors",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_images_product_id",
                table: "product_images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_sizes_product_id",
                table: "product_sizes",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipments_address_id",
                table: "shipments",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipments_order_id",
                table: "shipments",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_address_address_id",
                table: "user_address",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_address_user_id",
                table: "user_address",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "product_colors");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "product_sizes");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "shipments");

            migrationBuilder.DropTable(
                name: "user_address");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
