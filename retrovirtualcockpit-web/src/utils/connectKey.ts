const connectKey = {
    decode: function (clientCode: string): string {
        var decoded = ""
        var codex = ""

        for (var i1 = 0; i1 < 5; i1++) {
            codex += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        }

        for (var i2 = 0; i2 < 4; i2++) {
            var num = 16 * codex.indexOf(clientCode[0])

            codex = codex.slice(16)
            clientCode = clientCode.slice(1)

            num += codex.indexOf(clientCode[0])

            codex = codex.slice(16)
            clientCode = clientCode.slice(1)

            decoded += num

            if (i2 < 3) {
                decoded += "."
            }
        }

        return decoded
    }
}

export default connectKey