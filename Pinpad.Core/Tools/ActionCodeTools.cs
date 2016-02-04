using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class ActionCodeTools
    {
        public static string GetActionCodeDescription(string ActionCode)
        {
            switch (ActionCode)
            {
                case "00": return "Aprovado";
                case "01": return "Consultar emissor do cartão";
                case "02": return "Consultar emissor do cartão, condição especial";
                case "03": return "Transação inválida para o provedor do serviço";
                case "04": return "Reter cartão";
                case "05": return "Não honrar";
                case "06": return "Erro";
                case "07": return "Reter cartão, condição especial";
                case "08": return "Verificar com Identidade";
                case "10": return "Aprovado Parcialmente";
                case "11": return "Aprovado VIP";
                case "12": return "Transação Inválida";
                case "13": return "Valor inválido";
                case "14": return "Numero de cartão inválido - Vencimento incorreto";
                case "15": return "Emissor não encontrado";
                case "17": return "Cancelado pelo cliente";
                case "19": return "Tente novamente a transação";
                case "21": return "Nenhuma ação tomada";
                case "25": return "UNABLE TO LOCATE RECORD IN FILE, OR ACCOUNT NUMBER IS MISSING FROM";
                case "28": return "FILE IS TEMPORARILY UNAVAILABLE";
                case "30": return "Erro de formato";
                case "32": return "Reversal Parcial";
                case "34": return "Suspeita de Fraude";
                case "39": return "Sem conta de crédito (VISA EPAY)";
                case "41": return "Reter cartão (PERDIDO)";
                case "43": return "Reter cartão (ROUBADA)";
                case "51": return "Sem fundos";
                case "52": return "Sem conta corrente";
                case "53": return "Sem conta poupança";
                case "54": return "Cartão Vencido";
                case "55": return "Pin Incorreto";
                case "57": return "Transação não permitida para portador do cartão";
                case "58": return "Transação não permitida no terminal";
                case "61": return "Limite de movimentação de valor atingido";
                case "62": return "Cartão restrito";
                case "63": return "Violação de segurança";
                case "65": return "Limite de atividade da conta atingido";
                case "68": return "Resposta recebida com atraso";
                case "70": return "Contate o emissor do cartão";
                case "75": return "Atingido limite de tentativas de PIN";
                case "76": return "UNABLE TO LOCATE PREVIOUS MESSAGE (NO MATCH ON RETRIEVAL REFERENCE";
                case "77": return "PREVIOUS MESSAGE LOCATED FOR A REPEAT OR REVERSAL, BUT REPEAT OR R";
                case "80": return "Data inválida";
                case "81": return "Erro de cryptografia de PIN";
                case "82": return "CVV incorreto";
                case "83": return "Incapaz de verificar PIN";
                case "85": return "NO REASON TO DECLINE A REQUEST FOR ACCOUNT NUMBER VERIFICATION OR";
                case "86": return "Incapaz de validar PIN";
                case "88": return "Erro de cryptografia";
                case "89": return "PIN inaceitável";
                case "91": return "ISSUER OR SWITCH INOPERATIVE (STIP NOT APPLICABLE OR AVAILABLE FOR";
                case "92": return "DESTINATION CANNOT BE FOUND FOR ROUTING";
                case "93": return "TRANSACTION CANNOT BE COMPLETED, VIOLATION OF LAW";
                case "94": return "DUPLICATE TRANSMISSION DETECTED";
                case "95": return "SETTLEMENT DIFFERENCE";
                case "96": return "Falha no sistema ou erro em algum campo";
                case "AA": return "Cartão bloqueado";
                case "AB": return "Cartão cancelado";
                case "AC": return "Conta bloqueada";
                case "AD": return "Conta cancelada";
                case "AE": return "Cartão não ativado";
                case "KA": return "Canal não disponível";
                case "KB": return "BRAND RESPONSE TIMEOUT";
                case "N0": return "FORCE STIP";
                case "N3": return "Serviço de caixa não disponível";
                case "N4": return "Valor pedido excede limite do emissor";
                case "N7": return "Negado por falha de CVV2";
                case "P0": return "Aprovado, código PVID ausente, inválido ou expirado";
                case "P1": return "Negado, código PVID ausente, inválido ou expirado";
                case "P2": return "Informação do cobrador inválida";
                case "Q1": return "Falha de autenticação do cartão";
                case "there was no response from the authorization": return "Falha de rede no autorizador";
                default: return "Action Code não mapeado: " + ActionCode;
            }
        }
    }
}
