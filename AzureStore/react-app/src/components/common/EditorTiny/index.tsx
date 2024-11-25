import { Editor, IAllProps } from "@tinymce/tinymce-react";
import { FC } from "react";
import { httpService, BASE_URL } from '../../../api/http-service';
import { IProductImageDesc } from "../../../interfaces/products";

interface IEditorProps extends IAllProps {
  // onEditorChange *
  getSelectImage: (image: IProductImageDesc) => void;
  onEditorChange: (value: string) => void;
  value?: string;
}

const EditorTiny: FC<IEditorProps> = ({
  getSelectImage, 
  onEditorChange, 
  value,
  ...props
}) => {

  return (
        <Editor
          apiKey="wob1dim1h21fvpv4pf71g3y5x8u2inau7jnu00sf4ust5uex"
          // initialValue="<p>This is the initial content of the editor</p>"
          {...props}
          value={value}
          onEditorChange={onEditorChange} 
          init={{
            height: 300,
            language: "uk", // panel language
            menubar: true,
            images_file_types: "jpg,jpeg",
            block_unsupported_drop: false,
            menu: {
              file: { title: "File", items: "newdocument restoredraft | preview | print " },
              edit: { title: "Edit", items: "undo redo | cut copy paste | selectall | searchreplace" },
              view: { title: "View", items: "code | visualaid visualchars visualblocks | spellchecker | preview fullscreen" },
              insert: { title: "Insert", items: "image link media template codesample inserttable | charmap emoticons hr | pagebreak nonbreaking anchor toc | insertdatetime" },
              format: { title: "Format", items: "bold italic underline strikethrough superscript subscript codeformat | formats blockformats fontformats fontsizes align lineheight | forecolor backcolor | removeformat" },
              tools: { title: "Tools", items: "spellchecker spellcheckerlanguage | code wordcount" },
              table: { title: "Table", items: "inserttable | cell row column | tableprops deletetable" },
              help: { title: "Help", items: "help" }
            },
            plugins: [
              "image",
              "advlist autolink lists link image imagetools charmap print preview anchor",
              "searchreplace visualblocks code fullscreen textcolor ",
              "insertdatetime media table paste code help wordcount",
            ],
            toolbar:
              "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | forecolor backcolor",
            content_langs: [
              { title: "English", code: "en" },
              { title: "Українська", code: "ua" },
            ],
            content_style:
              "body { font-family:Helvetica,Arial,sans-serif; font-size:14px }",
              // upload images to server
              file_picker_callback: (cb, _value, _meta) => {
              const input = document.createElement("input");
              input.setAttribute("type", "file");
              input.setAttribute("accept", "image/*");
              input.addEventListener("change", (e: Event) => {
                const files = (e.target as HTMLInputElement).files;
                if (files) {
                  const file = files[0];
                  console.log("Selected file:", file);

                  const allowedTypes = ["image/jpeg", "image/png", "image/gif"];
                  if (!allowedTypes.includes(file.type)) {
                    alert("Unsupported file type");
                    return;
                  }
                  console.log("File select", file);

                  httpService
                    .post("api/products/uploads", { image: file },
                      {
                        headers: { "Content-Type": "multipart/form-data" }
                      })
                    .then((resp: any) => {
                      getSelectImage(resp.data);
                      const fileName =
                        BASE_URL + "/images/600_" + resp.data.image;
                      cb(fileName);
                      console.log("Resp data:", resp.data);

                    });

                }
                (e.target as HTMLInputElement).value = "";
              });

              input.click();
            },
          }}
          //toolbar="code"
          {...props}
        />
  );
};

export default EditorTiny;