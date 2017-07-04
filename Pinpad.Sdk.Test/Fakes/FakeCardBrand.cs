using Pinpad.Sdk.Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pinpad.Sdk.Test.Mockings
{
    public class CardBrandMocker
    {
        public static IList<PinpadCardBrand> GetMock ()
        {
            IList<PinpadCardBrand> cardBrands = new Collection<PinpadCardBrand>();

            // ID 1
            PinpadCardBrand cardBrand = new PinpadCardBrand();
            cardBrand.Id = 1;
            cardBrand.BrandType = TransactionType.Credit;
            cardBrand.Description = "VISA";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 3562900000000000000,
                FinalRange = 3779889999999999999,
                DataSetVersion = 0,
                TefBrandId = 1
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 4000060000000000000,
                FinalRange = 4999999999999999999,
                DataSetVersion = 0,
                TefBrandId = 1
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 9344020000000000000,
                FinalRange = 9752231299999999999,
                DataSetVersion = 0,
                TefBrandId = 1
            });

            cardBrands.Add(cardBrand);

            // ID 2
            cardBrand = new PinpadCardBrand();
            cardBrand.Id = 2;
            cardBrand.BrandType = TransactionType.Credit;
            cardBrand.Description = "MASTERCARD";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 0424100000000000000,
                FinalRange = 0424109999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 2001000000000000000,
                FinalRange = 2724815899999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 3528000000000000000,
                FinalRange = 3587969999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 5000048030000000000,
                FinalRange = 5990329999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6002060000000000000,
                FinalRange = 6026509999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6026520000000000000,
                FinalRange = 6033419999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6033430000000000000,
                FinalRange = 6280759999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 7039000000000000000,
                FinalRange = 7799999999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 8711199999900000000,
                FinalRange = 8788899999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 9188380000000000000,
                FinalRange = 9883889999999999999,
                DataSetVersion = 0,
                TefBrandId = 2
            });
            cardBrands.Add(cardBrand);

            // ID 3
            cardBrand = new PinpadCardBrand();
            cardBrand.Id = 3;
            cardBrand.BrandType = TransactionType.Debit;
            cardBrand.Description = "VISA ELECTRON";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 3562900000000000000,
                FinalRange = 3779889999999999999,
                DataSetVersion = 0,
                TefBrandId = 3
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 4000060000000000000,
                FinalRange = 4999999999999999999,
                DataSetVersion = 0,
                TefBrandId = 3
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 9344020000000000000,
                FinalRange = 9752231299999999999,
                DataSetVersion = 0,
                TefBrandId = 3
            });
            cardBrands.Add(cardBrand);

            // ID 4
            cardBrand = new PinpadCardBrand();
            cardBrand.Id = 4;
            cardBrand.BrandType = TransactionType.Debit;
            cardBrand.Description = "MAESTRO";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 0424100000000000000,
                FinalRange = 0424109999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 2001000000000000000,
                FinalRange = 2724815899999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 3528000000000000000,
                FinalRange = 3587969999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 5000048030000000000,
                FinalRange = 5990329999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6002060000000000000,
                FinalRange = 6026509999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6026520000000000000,
                FinalRange = 6033419999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6280770000000000000,
                FinalRange = 6818539999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6033430000000000000,
                FinalRange = 6280759999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 7039000000000000000,
                FinalRange = 7799999999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 8711199999900000000,
                FinalRange = 8788899999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 9188380000000000000,
                FinalRange = 9883889999999999999,
                DataSetVersion = 0,
                TefBrandId = 4
            });
            cardBrands.Add(cardBrand);

            // ID 5
            cardBrand = new PinpadCardBrand();
            cardBrand.Id = 5;
            cardBrand.BrandType = TransactionType.Debit;
            cardBrand.Description = "TICKET RESTAURANTE";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6033420000000000000,
                FinalRange = 6033429999999999999,
                DataSetVersion = 0,
                TefBrandId = 5
            });
            cardBrands.Add(cardBrand);

            // ID 6
            cardBrand = new PinpadCardBrand();
            cardBrand.Id = 6;
            cardBrand.BrandType = TransactionType.Debit;
            cardBrand.Description = "TICKET ALIMENTAÇÃO";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 6026510000000000000,
                FinalRange = 6026519999999999999,
                DataSetVersion = 0,
                TefBrandId = 6
            });
            cardBrands.Add(cardBrand);

            // ID 7
            cardBrand = new PinpadCardBrand();
            cardBrand.Id = 7;
            cardBrand.BrandType = TransactionType.Debit;
            cardBrand.Description = "TICKET CULTURA";
            cardBrand.Ranges = new Collection<PinpadBinRange>();
            cardBrand.Ranges.Add(new PinpadBinRange
            {
                InitialRange = 3085130000000000000,
                FinalRange = 3085139999999999999,
                DataSetVersion = 0,
                TefBrandId = 7
            });
            cardBrands.Add(cardBrand);

            return cardBrands;              
        }
    }
}
