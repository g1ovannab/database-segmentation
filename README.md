# Database Segmentation

Para realizar a segmentação dos dados em imagens MALIGNAS e BENIGNAS, é necessário executar o código desenvolvido.

Dado que o arquivo do banco de dados é extenso demais para ser versionado, é necessário baixá-lo separadamente e posicioná-lo no diretório `database-segmentation/DataSegmentation`. O arquivo deve ser posicionado nesse diretório COMPACTADO, ou seja, do jeito que foi instalado.

O código por sua vez, efetuará a separação das imagens em 2 diretórios distintos, os quais serão criados dentro de uma pasta especificada pela variável `destinationOfSegmentedImages` em `database-segmentation/DataSegmentation/DataSegmentation/Program.cs`. É fundamental ajustar essa variável para refletir o diretório desejado para o armazenamento das imagens segmentadas. Este procedimento visa otimizar o gerenciamento de grandes volumes de dados, permitindo a execução eficiente da segmentação sem a necessidade de incluir o arquivo de banco de dados no repositório de controle de versão.

Além disso, após executar o código uma vez, caso queira executar de novo, é recomendado comentar o método `UnzipFile` (uma vez que a descompactação dos arquivos demora cerca de 5 minutos).

---

## Hierarquia dos arquivos

O conteúdo da pasta descompactada derivada de `achive.zip` é dividido da seguinte maneira:

1. `achive\csv`

A pasta contém 4 arquivos **principais** em formato CSV:
- **calc_case_description_test_set**
- **calc_case_description_train_set**
- **mass_case_description_test_set**
- **mass_case_description_train_set**

Os arquivos que começam com **calc_** são imagens de calcificação. Em contrapartida, os arquivos que começam com **mass_** são imagens de massa. No caso do modelo desenvolvido, são usadas somente imagens de massa.

As principais colunas dos arquivos são definidas em (das quais somente colunas em negrito são usadas no código da segmentação):
- **patient_id** - id do paciente
- **pathology**	
- image file path (imagens completas)
- **cropped image file path** (imagens cortadas, com foco na massa)
- ROI mask file path (imagens de região de interesse)

A coluna `cropped image file path` indica o caminho da imagem, dentro da pasta `jpeg`.


2. `achive\jpeg`

A pasta `jpeg` abriga diversas subpastas, definidas nos campos de path dos arquivos CSV na qual cada subpasta contém as imagens indicadas. 