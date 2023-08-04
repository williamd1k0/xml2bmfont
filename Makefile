TARGET = Release
SRC_DIR = FontXmlToTextConverter
SRC = $(shell find ${SRC_DIR} -type f -name '*.cs' -o -name '*.csproj')
BIN = xml2bmfont.exe
OUT = ${SRC_DIR}/bin/${TARGET}/${BIN}

build: ${OUT}

clean:
	rm -r -- ${SRC_DIR}/bin ${SRC_DIR}/obj

${OUT}: FontXmlToTextConverter.sln ${SRC}
	msbuild $< -p:Configuration=${TARGET}

.PHONY: clean
