using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class CardInfo
    {
        public string Number = "";
        public string Holder = "";
        public string Expiry = "";
        public string CVV = "";
    }

    public class TransferInfo
    {
        public string Data = "";
    }

    public class CashInfo
    {
        public string CashierName = "";
    }

    public interface IPaymentMethod
    {
        bool ProcessPayment(double amount);
    }
    public interface ICardDataInput
    {
        void InputData(string number, string holder, string expiry, string cvv);
    }
    public interface ITransferDataInput
    {
        void InputData(string data);
    }
    public interface ICashDataInput
    {
        void InputData(string cashierName);
    }
    public interface IValidator
    {
        bool Validate();
    }
    public class CreditCardPayment : IPaymentMethod, ICardDataInput, IValidator
    {
        private CardInfo _card = new CardInfo();

        public void InputData(string number, string holder, string expiry, string cvv){
            _card.Number = number;
            _card.Holder = holder;
            _card.Expiry = expiry;
            _card.CVV = cvv;
        }


        public bool Validate()
        {
            bool ok =
                !string.IsNullOrWhiteSpace(_card.Number) &&
                !string.IsNullOrWhiteSpace(_card.Holder) &&
                !string.IsNullOrWhiteSpace(_card.Expiry) &&
                !string.IsNullOrWhiteSpace(_card.CVV);

            if (!ok) Console.WriteLine("Validación: faltan datos de tarjeta.");
            return ok;
        }

        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Pagando {amount} con TARJETA DE CREDITO {_card.Number}.");
            return true;
        }
    }

    public class DebitCardPayment : IPaymentMethod, ICardDataInput, IValidator
    {
        private CardInfo _card = new CardInfo();

        public void InputData(string number, string holder, string expiry, string cvv)
        {
            _card.Number = number;
            _card.Holder = holder;
            _card.Expiry = expiry;
            _card.CVV = cvv;
        }

        public bool Validate() // required major validation as if the inputs is number, string, date, etc. :3
        {
            bool ok =
                !string.IsNullOrWhiteSpace(_card.Number) &&
                !string.IsNullOrWhiteSpace(_card.Holder) &&
                !string.IsNullOrWhiteSpace(_card.Expiry) &&
                !string.IsNullOrWhiteSpace(_card.CVV);

            if (!ok) Console.WriteLine("Validación: faltan datos de tarjeta.");
            return ok;
        }

        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Pagando {amount} con TARJETA DE DÉBITO {_card.Number}.");
            return true;
        }
    }

    public class CashPayment : IPaymentMethod, ICashDataInput, IValidator
    {
        private CashInfo _cash = new CashInfo();

        public void InputData(string cashierName)
        {
            _cash.CashierName = cashierName;
        }
        
        public bool Validate()
        {
            bool ok = !string.IsNullOrWhiteSpace(_cash.CashierName);
            if (!ok) Console.WriteLine("Validación: nombre de cajero requerido.");
            return ok;
        }

        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Pagando {amount} en EFECTIVO atendido por {_cash.CashierName}.");
            return true;
        }
    }
    public class YapeNumberPayment : IPaymentMethod, ITransferDataInput, IValidator
    {
        private TransferInfo _transfer = new TransferInfo();

        public void InputData(string data)
        {
            _transfer.Data = data;
        }

        public bool Validate()
        {
            bool ok = !string.IsNullOrWhiteSpace(_transfer.Data);
            if (!ok) Console.WriteLine("Validación: número de celular Yape requerido.");
            return ok;
        }

        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Transfiriendo {amount} a número Yape {_transfer.Data}.");
            return true;
        }
    }
    public class YapeQRPayment : IPaymentMethod, ITransferDataInput, IValidator
    {
        private TransferInfo _transfer = new TransferInfo();

        public void InputData(string data)
        {
            _transfer.Data = data;
        }

        public bool Validate()
        {
            bool ok = !string.IsNullOrWhiteSpace(_transfer.Data);
            if (!ok) Console.WriteLine("Validación: código QR Yape requerido.");
            return ok;
        }

        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Transfiriendo {amount} escaneando QR: {_transfer.Data} (Yape).");
            return true;
        }
    }
    public class PlinTransfer : IPaymentMethod, ITransferDataInput, IValidator
    {
        private TransferInfo _transfer = new TransferInfo();

        public void InputData(string data)
        {
            _transfer.Data = data;
        }

        public bool Validate()
        {
            bool ok = !string.IsNullOrWhiteSpace(_transfer.Data);
            if (!ok) Console.WriteLine("Validación: número de celular Plin requerido.");
            return ok;
        }

        public bool ProcessPayment(double amount)
        {
            Console.WriteLine($"Transfiriendo {amount} a número Plin {_transfer.Data}.");
            return true;
        }
    }

    public class PaymentProcessor
    {
        private IPaymentMethod _paymentMethod;
        public PaymentProcessor(IPaymentMethod paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        public bool Pay(double amount)
        {
            Console.WriteLine("Procesando pago...");
            if (_paymentMethod is IValidator validator && !validator.Validate())
            {
                Console.WriteLine("Pago fallido: validación no aprobada.");
                return false;
            }
            if (_paymentMethod.ProcessPayment(amount))
            {
                Console.WriteLine("Pago procesado exitosamente.");
                return true;
            }
            else
            {
                Console.WriteLine("Pago fallido: error al procesar el pago.");
                return false;
            }
        }
    }


}

