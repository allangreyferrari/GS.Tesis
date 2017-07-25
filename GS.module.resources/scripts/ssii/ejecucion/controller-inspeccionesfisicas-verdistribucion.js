$(document).ready(function () {
    var observable = kendo.observable({
        source: new kendo.data.DataSource({
            data: [
                      {
                          "responsable": "Allan Davis Grey",
                          "inspecciones": [
                              {
                                  "turno": "1A1",
                                  "agente": "PARTNER LOGISTIC MGN",
                                  "tiempo_espera": 3,
                                  "revision_documentaria": false,
                                  "revision_fisica": false,
                                  "dua": "45131242"
                              },
                              {
                                  "turno": "1A2",
                                  "agente": "KUEHNE NAGEL SA",
                                  "tiempo_espera": 2,
                                  "revision_documentaria": true,
                                  "revision_fisica": true,
                                  "dua": "12453512"
                              }
                          ]
                      },
                      {
                          "responsable": "Antonio Rodriguez",
                          "inspecciones": [
                              {
                                  "turno": "1A1",
                                  "agente": "PARTNER LOGISTIC MGN",
                                  "tiempo_espera": 3,
                                  "revision_documentaria": true,
                                  "revision_fisica": false,
                                  "dua": "45131242"
                              }
                          ]
                      },
                      {
                          "responsable": "Lucas Vasquez",
                          "inspecciones": [
                              {
                                  "turno": "1A1",
                                  "agente": "PARTNER LOGISTIC MGN",
                                  "tiempo_espera": 3,
                                  "revision_documentaria": true,
                                  "revision_fisica": false,
                                  "dua": "45131242"
                              }
                          ]
                      }
            ]
        })
    });
    kendo.bind(document.body, observable);
});

function Regresar() {
    argument = null;
    $("#pages").load('../../Content/ssii/ejecucion/viewinspeccionesfisicas.html');
}