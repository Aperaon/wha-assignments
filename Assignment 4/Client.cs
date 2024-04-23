using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientXie{

    public class Client{

        private string _firstName; 
        private string _lastName;
        private double _weight;
        private double _height;

        public Client(){
            _firstName = "XXX";
            _lastName = "XXX";  
            _weight = 0;
            _height = 0;
        }

        public Client(string firstName, string lastName, double weight, double height){
        
            FirstName = firstName;
            LastName = lastName;
            Weight = weight;
            Height = height;

        }

        public string FirstName {   
            get{ 
                return _firstName; 
            }  
            set{ 
                if(string.IsNullOrEmpty(value)) {
                    throw new ArgumentNullException("First name can not be empty or blank");
                }else{
                    _firstName = value.Trim();
                }
            }
        }

        public string LastName {
            get{ 
                return _lastName; 
            }  
            set{ 
                if(string.IsNullOrEmpty(value)) {
                    throw new ArgumentNullException("First name can not be empty or blank");
                }else{
                    _lastName = value.Trim();
                }
            }
        }

        public double Weight {
            get {
                return _weight;
            }
            set{
                if(value <= 0){
                    throw new ArgumentOutOfRangeException("Value must be greater than 0");
                }else{
                    _weight = value;
                }
            }
        }

        public double Height {
            get{
                return _height;
            }

            set{
                if(value <= 0){
                    throw new ArgumentOutOfRangeException("Value must be greater than 0");
                }else{
                    _height = value;
                }
            }
        }

        public double bmiScore {
            get{

                double bmiWeight = Weight / (Height * Height) * 703;
                return bmiWeight;

            }
        }

        public string bmiStatus {
            get{

                double Score = bmiScore;
                string Status;

                if (Score <= 18.4){
                    Status = "Underweight";
                }else if(Score <= 24.9){
                    Status = "Normal";
                }else if(Score <= 39.9){
                    Status = "Overweight";
                }else{
                    Status = "Obese";
                }

                return Status;

            }
        }

        public string fullName{
            get{

                string fullName = $"{LastName}, {FirstName}";
                return fullName;

            }
        }

        public override string ToString()
		{
			return $"{FirstName},{LastName},{Weight},{Height}";
		}

    }

}
