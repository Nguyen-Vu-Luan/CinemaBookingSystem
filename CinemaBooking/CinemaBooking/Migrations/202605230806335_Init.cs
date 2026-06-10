namespace CinemaBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GheNgois",
                c => new
                    {
                        MaGhe = c.Int(nullable: false, identity: true),
                        MaSuatChieu = c.Int(nullable: false),
                        ViTriGhe = c.String(),
                        TrangThai = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaGhe)
                .ForeignKey("dbo.SuatChieux", t => t.MaSuatChieu)
                .Index(t => t.MaSuatChieu);
            
            CreateTable(
                "dbo.SuatChieux",
                c => new
                    {
                        MaSuatChieu = c.Int(nullable: false, identity: true),
                        MaPhim = c.Int(nullable: false),
                        ThoiGianChieu = c.DateTime(nullable: false),
                        PhongChieu = c.String(),
                        GiaVe = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaSuatChieu)
                .ForeignKey("dbo.Phims", t => t.MaPhim)
                .Index(t => t.MaPhim);
            
            CreateTable(
                "dbo.Phims",
                c => new
                    {
                        MaPhim = c.Int(nullable: false, identity: true),
                        TenPhim = c.String(),
                        HinhAnh = c.String(),
                        DaoDien = c.String(),
                        ThoiLuong = c.Int(nullable: false),
                        MaTheLoai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaPhim)
                .ForeignKey("dbo.TheLoais", t => t.MaTheLoai)
                .Index(t => t.MaTheLoai);
            
            CreateTable(
                "dbo.TheLoais",
                c => new
                    {
                        MaTheLoai = c.Int(nullable: false, identity: true),
                        TenTheLoai = c.String(),
                    })
                .PrimaryKey(t => t.MaTheLoai);
            
            CreateTable(
                "dbo.NguoiDungs",
                c => new
                    {
                        MaND = c.Int(nullable: false, identity: true),
                        TaiKhoan = c.String(),
                        MatKhau = c.String(),
                        HoTen = c.String(),
                        VaiTro = c.String(),
                    })
                .PrimaryKey(t => t.MaND);
            
            CreateTable(
                "dbo.Ves",
                c => new
                    {
                        MaVe = c.Int(nullable: false, identity: true),
                        MaND = c.Int(nullable: false),
                        MaSuatChieu = c.Int(nullable: false),
                        MaGhe = c.Int(nullable: false),
                        NgayDat = c.DateTime(nullable: false),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaVe)
                .ForeignKey("dbo.GheNgois", t => t.MaGhe)
                .ForeignKey("dbo.NguoiDungs", t => t.MaND)
                .ForeignKey("dbo.SuatChieux", t => t.MaSuatChieu)
                .Index(t => t.MaND)
                .Index(t => t.MaSuatChieu)
                .Index(t => t.MaGhe);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ves", "MaSuatChieu", "dbo.SuatChieux");
            DropForeignKey("dbo.Ves", "MaND", "dbo.NguoiDungs");
            DropForeignKey("dbo.Ves", "MaGhe", "dbo.GheNgois");
            DropForeignKey("dbo.Phims", "MaTheLoai", "dbo.TheLoais");
            DropForeignKey("dbo.SuatChieux", "MaPhim", "dbo.Phims");
            DropForeignKey("dbo.GheNgois", "MaSuatChieu", "dbo.SuatChieux");
            DropIndex("dbo.Ves", new[] { "MaGhe" });
            DropIndex("dbo.Ves", new[] { "MaSuatChieu" });
            DropIndex("dbo.Ves", new[] { "MaND" });
            DropIndex("dbo.Phims", new[] { "MaTheLoai" });
            DropIndex("dbo.SuatChieux", new[] { "MaPhim" });
            DropIndex("dbo.GheNgois", new[] { "MaSuatChieu" });
            DropTable("dbo.Ves");
            DropTable("dbo.NguoiDungs");
            DropTable("dbo.TheLoais");
            DropTable("dbo.Phims");
            DropTable("dbo.SuatChieux");
            DropTable("dbo.GheNgois");
        }
    }
}
