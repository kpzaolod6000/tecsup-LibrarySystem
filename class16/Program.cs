using class16.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Crear una función para el metodo o tipy de pago usando principio SOLID
// Crear una función para el descuento especial a cierto producto
// Crear una función de venta para productos físicos y virtuales
// Crear roles de usuario(proveedor, cliente, vendedor) mediante interfaces la función gestionar pedido

namespace class16
{
    class Program
    {
        static void Main()
        {
            IProductRepository repo = new ProductRepository();
            DiscountService discountService = new DiscountService();
            PaymentService paymentService = new PaymentService();
            SaleService saleService = new SaleService(discountService, paymentService);

            Admin admin = new Admin(repo);
            admin.InputData("AdminTest", "admin@test.com");
            Provider provider = new Provider(repo);
            provider.InputData("ProviderTest", "provider@test.com", "PID001");
            Client client = new Client();
            client.InputData("ClientTest", "client@test.com", "123 Main St");
            Vendor vendor = new Vendor();
            vendor.InputData("VendorTest", "vendor@test.com", "VEND001");

            while (true)
            {
                Console.WriteLine("\n--- Menú de Roles ---");
                Console.WriteLine("1) Admin\n2) Provider\n3) Client\n4) Vendor\n0) Salir");
                Console.Write("Seleccione rol: ");
                string rol = Console.ReadLine();
                if (rol == "0") break;

                switch (rol)
                {
                    case "1":
                        Console.Write("Nombre Admin: ");
                        string aName = Console.ReadLine();
                        Console.Write("Email Admin: ");
                        string aEmail = Console.ReadLine();
                        admin.InputData(aName, aEmail);

                        Console.WriteLine("1) Crear producto\n2) Actualizar producto");
                        string aOp = Console.ReadLine();

                        if (aOp == "1")
                        {
                            Console.WriteLine("Tipo: 1) Book 2) Notebook 3) Pencil");
                            string t = Console.ReadLine();
                            Console.Write("Nombre: ");
                            string n = Console.ReadLine();
                            Console.Write("Precio: ");
                            double pr = double.Parse(Console.ReadLine());
                            Console.Write("Stock inicial: ");
                            int st = int.Parse(Console.ReadLine());

                            Product p;
                            if (t == "1")
                                p = new Book(n, pr, "AutorX", "ISBNX", st);
                            else if (t == "2")
                                p = new Notebook(n, pr, 100, "A4", st);
                            else
                                p = new Pencil(n, pr, "MarcaX", "HB", st);

                            admin.CreateProduct(p);
                        }
                        else if (aOp == "2")
                        {
                            List<Product> all = repo.GetAll().ToList();
                            for (int i = 0; i < all.Count; i++)
                            {
                                Console.Write((i + 1) + ") ");
                                all[i].PrintDetails();
                            }
                            Console.Write("Seleccione índice: ");
                            int idx = int.Parse(Console.ReadLine()) - 1;
                            Product prod = all[idx];
                            Console.Write("Nuevo precio: ");
                            prod.Price = double.Parse(Console.ReadLine());
                            Console.Write("Nuevo stock: ");
                            prod.Stock = int.Parse(Console.ReadLine());
                            admin.UpdateProduct(prod);
                        }
                        break;

                    case "2":
                        Console.Write("Nombre Provider: ");
                        string pName = Console.ReadLine();
                        Console.Write("Email Provider: ");
                        string pEmail = Console.ReadLine();
                        Console.Write("PersonalId: ");
                        string pid = Console.ReadLine();
                        provider.InputData(pName, pEmail, pid);

                        List<Product> list = repo.GetAll().ToList();
                        for (int i = 0; i < list.Count; i++)
                        {
                            Console.Write((i + 1) + ") ");
                            list[i].PrintDetails();
                        }

                        Console.Write("Índice para reponer: ");
                        int idp = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Cantidad a reponer: ");
                        int qty = int.Parse(Console.ReadLine());
                        provider.ProvideStock(list[idp].Id, qty);
                        break;

                    case "3":
                        Console.Write("Nombre Cliente: ");
                        string cName = Console.ReadLine();
                        Console.Write("Email Cliente: ");
                        string cEmail = Console.ReadLine();
                        Console.Write("Dirección envío: ");
                        string addr = Console.ReadLine();
                        client.InputData(cName, cEmail, addr);

                        Order order = new Order();
                        List<Product> prodList = repo.GetAll().ToList();
                        for (int i = 0; i < prodList.Count; i++)
                        {
                            Console.Write((i + 1) + ") ");
                            prodList[i].PrintDetails();
                        }

                        Console.WriteLine("Indices (coma separados): ");
                        string[] choices = Console.ReadLine().Split(',');
                        foreach (string ch in choices)
                        {
                            int index = int.Parse(ch.Trim()) - 1;
                            order.Add(prodList[index]);
                        }

                        Console.WriteLine("Descuento: 1) Ninguno 2) Cliente 3) Temporada 4) Cupón");
                        string dc = Console.ReadLine();
                        if (dc == "2") discountService.RegisterDiscount(new ClientTypeDiscount());
                        else if (dc == "3") discountService.RegisterDiscount(new SummerSeasonDiscount());
                        else if (dc == "4")
                        {
                            Console.Write("Tasa cupón: ");
                            double tasa = double.Parse(Console.ReadLine());
                            discountService.RegisterDiscount(new CouponDiscount(tasa));
                        }

                        Console.WriteLine("Pago: 1) Tarjeta 2) Débito 3) Efectivo 4) Yape Num 5) Yape QR 6) Plin");
                        string mp = Console.ReadLine();

                        IPaymentMethod pay;
                        if (mp == "1") pay = new CreditCardPayment();
                        else if (mp == "2") pay = new DebitCardPayment();
                        else if (mp == "4") pay = new YapeNumberPayment();
                        else if (mp == "5") pay = new YapeQRPayment();
                        else if (mp == "6") pay = new PlinTransfer();
                        else pay = new CashPayment();

                        ICardDataInput ci = pay as ICardDataInput;
                        if (ci != null)
                        {
                            Console.Write("Num tarjeta: ");
                            ci.InputData(Console.ReadLine(), "Holder", "Expiry", "CVV");
                        }

                        ITransferDataInput ti = pay as ITransferDataInput;
                        if (ti != null)
                        {
                            Console.Write("Dato transfer: ");
                            ti.InputData(Console.ReadLine());
                        }

                        ICashDataInput ci2 = pay as ICashDataInput;
                        if (ci2 != null)
                        {
                            Console.Write("Nombre cajero: ");
                            ci2.InputData(Console.ReadLine());
                        }

                        Console.Write("Venta online? (s/n): ");
                        bool onl = Console.ReadLine().ToLower() == "s";
                        client.PlaceOrder(order, pay, saleService, vendor, onl);
                        break;

                    case "4":
                        Console.Write("Nombre Vendor: ");
                        string vName = Console.ReadLine();
                        Console.Write("Email Vendor: ");
                        string vEmail = Console.ReadLine();
                        Console.Write("Código: ");
                        string vCode = Console.ReadLine();
                        vendor.InputData(vName, vEmail, vCode);
                        Order o2 = new Order();
                        o2.Add(repo.GetAll().First());
                        vendor.MakeSale(o2, new CashPayment(), saleService, client, true);
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }

        }
    }
}