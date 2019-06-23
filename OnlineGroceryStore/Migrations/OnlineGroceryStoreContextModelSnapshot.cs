﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineGroceryStore.Models;

namespace OnlineGroceryStore.Migrations
{
    [DbContext(typeof(OnlineGroceryStoreContext))]
    partial class OnlineGroceryStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("OnlineGroceryStore.Models.Inventory", b =>
                {
                    b.Property<string>("itemID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("itemCode");

                    b.Property<string>("itemName");

                    b.Property<int>("stockLevel");

                    b.HasKey("itemID");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("OnlineGroceryStore.Models.InventoryPackingConfigure", b =>
                {
                    b.Property<int>("packingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("itemID");

                    b.Property<double>("packPrice");

                    b.Property<int>("packSize");

                    b.HasKey("packingID");

                    b.HasIndex("itemID");

                    b.ToTable("InventoryPackingConfigure");
                });

            modelBuilder.Entity("OnlineGroceryStore.Models.InventoryPackingConfigure", b =>
                {
                    b.HasOne("OnlineGroceryStore.Models.Inventory", "inventory")
                        .WithMany("packs")
                        .HasForeignKey("itemID");
                });
#pragma warning restore 612, 618
        }
    }
}