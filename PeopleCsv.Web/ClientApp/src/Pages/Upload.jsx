import {useRef} from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});
const Upload=()=>{
    const fileRef = useRef();
    const navigate=useNavigate();

    const onUploadClick = async () => {
        if (!fileRef.current.files.length) {
            return;
        }
        const file = fileRef.current.files[0];
        const base64 = await toBase64(file);
        await axios.post('/api/file/upload', {
            base64data: base64
        });
        navigate('/');
      
    }
    


    return(
        <div className='container' style={{marginTop:60}}>
            <div className='d-flex vh-100' style={{marginTop:-70}}>
                <div className='d-flex w-100 justify-content-center align-self-center'>
                    <div className='row'>
                        <div className='col-md-10'>
                            <input type='file' ref={fileRef} className='form-control'></input>
                        </div>
                        <div className='col-md-2'>
                            <button onClick={onUploadClick} className='btn btn-primary'>Upload</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    )

}
export default Upload;