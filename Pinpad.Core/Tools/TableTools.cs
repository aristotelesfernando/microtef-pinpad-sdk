using CrossPlatformBase;
using PinPadSDK.Commands;
using PinPadSDK.Commands.Tables;
using PinPadSDK.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PinPadSDK.Tools
{
    public class TableTools
    {
        public static string GetTableVersion(PinPadConnectionController writeTool)
        {
            GTSRequest request = new GTSRequest();
            request.GTS_ACQIDX = 0;
            //Identificador da Rede Credenciadora referente às Tabelas EMV cuja versão está sendo requisitada.
            //Deve-se usar o valor “00” quando se utiliza uma versão de tabelas única para todas as redes (isso só faz sentido se as tabelas foram carregadas usando-se também “00” no comando “TLI”).

            GTSResponse response = writeTool.WriteRequest<GTSResponse>(request);
            if (response == null || response.RSP_STAT != ReturnCodes.ST_OK)
                return null;
            else
                return response.GTS_TABVER;
        }
    }
}
