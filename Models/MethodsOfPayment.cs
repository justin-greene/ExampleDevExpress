using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication5.Models
{
    public static class MethodsOfPayment
    {
        public const string AMEX1_CC = "Amex1 Credit Card";
        public const string AMEX2_CC = "Amex2 Credit Card";
        public const string DIRECT_BILL_CHECK = "Direct Bill to CLS";
        public const string DIRECT_BILL_VISA_VCARD = "Direct Bill to Visa VCard";
        public const string DIRECT_BILL_COMDATA_VCARD = "Direct Bill to Comdata VCard";
        public const string VISA_CC = "Visa Credit Card";
        public const string VISA_VCARD = "Visa VCard Credit Card";
        public const string COMDATA_VCARD = "Comdata VCard Credit Card";

        /// <summary>
        /// Returns all valid billing strings.
        /// </summary>
        public static IEnumerable<string> AllTypes = new string[] {
             AMEX1_CC,
             AMEX2_CC,
             DIRECT_BILL_CHECK,
             DIRECT_BILL_VISA_VCARD,
             DIRECT_BILL_COMDATA_VCARD,
             VISA_CC,
             VISA_VCARD,
             COMDATA_VCARD
        };

        /// <summary>
        /// Contains all billing types that are considered direct billing, including CDBs.
        /// </summary>
        public static IEnumerable<string> DirectBillTypes = new string[] {
            DIRECT_BILL_CHECK,
            DIRECT_BILL_VISA_VCARD,
            DIRECT_BILL_COMDATA_VCARD
        };

        /// <summary>
        /// Contains all standard (non-vcard) credit card billing types for temporary hotels.
        /// </summary>
        public static IEnumerable<string> StandardCreditCardTypes = new string[] {
            AMEX1_CC,
            AMEX2_CC,
            VISA_CC
        };

        /// <summary>
        /// Contains all post-pay (direct bill) vcard payment types.
        /// </summary>
        public static IEnumerable<string> PostPayVCardTypes = new string[] {
            DIRECT_BILL_VISA_VCARD,
            DIRECT_BILL_COMDATA_VCARD
        };

        /// <summary>
        /// Contains both the Direct Bill Types and the Post Pay Vcard Types. 
        /// </summary>
        public static IEnumerable<string> PriorityPayVCardTypes = PostPayVCardTypes.Union(DirectBillTypes);

        /// <summary>
        /// Contains all pre-pay (temporary) vcard payment types.
        /// </summary>
        public static IEnumerable<string> PrePayVCardTypes = new string[] {
            VISA_VCARD,
            COMDATA_VCARD
        };

        /// <summary>
        /// Contains all credit card billing types for temporary hotels.
        /// </summary>
        public static IEnumerable<string> PrePayCreditCardTypes = StandardCreditCardTypes.Union(PrePayVCardTypes);

        /// <summary>
        /// Contains all credit card billing types for direct bill hotels.
        /// </summary>
        public static IEnumerable<string> PostPayCreditCardTypes = PostPayVCardTypes;

        /// <summary>
        /// Contains all credit card billing types.
        /// </summary>
        public static IEnumerable<string> AllCreditCardTypes = PrePayCreditCardTypes.Union(PostPayVCardTypes);

        /// <summary>
        /// Contains all billing types for which hotel invoices get integrated into Great Plains.
        /// </summary>
        public static IEnumerable<string> IntegratedTypes = AllTypes.Except(AllCreditCardTypes);


        /// <summary>
        /// Contains all Visa VCard billing types - direct bill and temporary.
        /// </summary>
        public static IEnumerable<string> VisaVCardTypes = new string[] {
            VISA_VCARD,
            DIRECT_BILL_VISA_VCARD
        };

        /// <summary>
        /// Contains all Comdata VCard billing types - direct bill and temporary.
        /// </summary>
        public static IEnumerable<string> ComdataVCardTypes = new string[] {
            COMDATA_VCARD,
            DIRECT_BILL_COMDATA_VCARD
        };

        /// <summary>
        /// The amex vcard types
        /// </summary>
        /// <remarks>
        /// David Moore, JT-1565, 10/28/2015, Added for amex cards
        /// </remarks>
        public static IEnumerable<string> AMEXVCardTypes = new string[]
        {
            AMEX1_CC,
            AMEX2_CC
        };

        /// <summary>
        /// The BBTV card types
        /// </summary>
        /// <remarks>
        /// David Moore, JT-1565, 10/28/2015, Added for bbt cards
        /// </remarks>
        public static IEnumerable<string> BBTVCardTypes = new string[]
        {
            VISA_CC
        };

        /// <summary>
        /// Returns a simplified description of the billing type.  Usually, "check" or "credit card".
        /// </summary>
        /// <param name="billing">A Billing string.  Should be a const in the MethodsOfPayment class.</param>
        public static string ToSimpleDesc(string billing)
        {
            if (AllCreditCardTypes.Contains(billing))
                return "Credit Card";
            else if (billing == DIRECT_BILL_CHECK)
                return "Check";
            else
                return billing;
        }

        /// <summary>
        /// Gets a sequence of all hotel invoice number prefixes for pre-pay credit card invoices.
        /// </summary>
        public static IEnumerable<string> CreditCardInvoiceNumberPrefixes
        {
            get
            {
                return PrePayCreditCardTypes.Select(mop => mop.Substring(0, 4).ToUpper()).Distinct(); ;
            }
        }
    }
}