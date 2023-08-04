TARGET = Release
SRC_DIR = FontXmlToTextConverter
SRC = $(shell find ${SRC_DIR} -type f -name '*.cs' -o -name '*.csproj')
BIN = xml2bmfont
OUT_DIR = ${SRC_DIR}/bin/${TARGET}
OUT = ${OUT_DIR}/${BIN}

build: ${OUT} ${OUT}.exe

clean:
	rm -r -- ${SRC_DIR}/bin ${SRC_DIR}/obj

${OUT}.exe: FontXmlToTextConverter.sln ${SRC}
	msbuild $< -p:Configuration=${TARGET}

${OUT}: ${SRC_DIR}/${BIN}
	cp $< $@

.PHONY: clean
