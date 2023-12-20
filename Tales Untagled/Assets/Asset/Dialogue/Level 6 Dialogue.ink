INCLUDE Globals.ink

{ActiveEndGame == "": -> main | -> alreadyChose}

=== main === 

Terima kasih sudah memanggil <color=\#DB1D00>Samosir</color> untuk kami.#speaker:<color=\#88BDFF>Toba </color> #potrait:Toba #layout:right

Tidak apa-apa. Aku senang bisa membantu.#speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Oh, ya. Aku ingin memperkenalkanmu kepada istriku, <color=\#F0C31E>Mina</color>.#speaker:<color=\#88BDFF>Toba </color> #potrait:Toba #layout:right

Halo, Adi. Selamat datang di rumah kami. #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right

Halo, Bu. Terima kasih atas sambutannya. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Nah, sekarang kami akan membantumu. Aku yakin kamu bingung bagaimana kamu bisa tiba-tiba berada di sini dan ingin kembali kan?#speaker:<color=\#88BDFF>Toba </color> #potrait:Toba #layout:right
    *[Benar]
        Benar, apakah kalian bisa membantu? #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
        
        Kami akan mencoba membantumu, apakah kamu bisa menjelaskan bagaimana kamu bisa berada di sini? ##speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right
    *[Tapi]
        Tapi apakah memang aku ke sini karena suatu hal mistik dan apakah aku bisa kembali dari sini? #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
        
        Mungkin saja, hal mistik bisa terjadi kepada siapa saja. Apakah kamu bisa menjelaskan bagaimana kamu bisa berada di sini? #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right
        
- Saya tiba-tiba terbangun di sini setelah membuka sebuah buku di rumah, #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
hal terakhir yang aku ingat adalah buku yang aku buka bersinar sebelum saya terbangun di sini.  #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Itu terdengar seperti hal mistik, #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right
jika mengingat penjebabnya seharusnya kamu perlu mencari hal yang berhubungan dengan buku yang kamu buka. #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right

Aku ada menemukan potongan kertas sepanjang perjalananku, apakah ini yang dimaksud? #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Aku merasakan aura mistik dari kertas-kertas ini. Kamu bisa mencoba untuk mengumpulkan kertas-kertas ini, #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right
seharusnya setelah kamu mengumpulkan jumlah tertentu, kamu bisa kembali ke tempat asalmu. #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right

Berarti saya perlu berkeliling untuk mencari kertas-kertas yang lain? #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Tampaknya begitu. Tapi, aku yakin kamu akan menemukannya. #speaker:<color=\#88BDFF>Toba </color> #potrait:Toba #layout:right

Baiklah, Pak. Saya akan berusaha keras untuk menemukannya. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Semoga kau berhasil. Jika kamu sudah menemukan cukup banyak, datanglah kembali ke sini. Kami akan membantumu pulang. ##speaker:<color=\#88BDFF>Toba </color> #potrait:Toba #layout:right

Terima kasih, Pak. Saya akan berusaha keras untuk menemukannya. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Tapi untuk sekarang, kamu boleh beristirahat di sini terlebih dahulu dan kamu bisa mencarinya besok. #speaker:<color=\#F0C31E>Mina</color> #potrait:Mina #layout:right

Terima kasih. Aku sangat menghargainya.  #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
~ ActiveEndGame = "Open"
-> DONE

=== alreadyChose === 

silahkan beristirahatlah, kamu pasti sangat kelelahan dalam perjalanan kemari #speaker:<color=\#88BDFF>Toba </color> #potrait:Toba #layout:right
->DONE